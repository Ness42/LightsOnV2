// Visual Micro is in vMicro>General>Tutorial Mode
// 
/*
    Name:       LightsOn_ArduinoYun.ino
    Created:	03.11.2019 13:49:06
    Author:     DESKTOP-RF1F593\markw
*/

// Define User Types below here or use a .h file
//


// Define Function Prototypes that use User Types below here or use a .h file
//


// Define Functions below here or use other .ino or cpp files
//







#include <Bridge.h>
#include <BridgeClient.h>
#include <BridgeServer.h>
#include <BridgeSSLClient.h>
#include <BridgeUdp.h>
#include <Console.h>
#include <FileIO.h>
#include <HttpClient.h>
#include <Mailbox.h>
#include <Process.h>


#include <SPI.h>
#include <Ethernet.h>
#include <IRremoteMarc.h>
#include <Wire.h>  // Library which contains functions to have I2C Communication
#include <SoftwareSerial.h>
#include <EEPROM.h>


#define SLAVE_ADDRESS 0x40 // Define the I2C address to Communicate to Uno
#define aref_voltage 3.3         // we tie 3.3V to ARef and measure it with a multimeter!


#define rxPin 12
#define txPin 13
#define houseCode1 0xFF
#define houseCode2 0xFD

SoftwareSerial softSerial = SoftwareSerial(rxPin, txPin);

// network configuration.  gateway and subnet are optional.
//byte mac[] = {0x90, 0xA2, 0xDA, 0xF0, 0x18, 0xE8};
byte mac[] = {0x90, 0xA2, 0xDA, 0xF8, 0x0D, 0x68};
//IPAddress ip(192, 168, 1, 73);
IPAddress ip(192, 168, 1, 92);
IPAddress myDns(192,168,1, 1);
IPAddress gateway(192, 168, 1, 2);
IPAddress subnet(255, 255, 255, 0);
// telnet defaults to port 23
//EthernetServer server(1337);
BridgeServer server(1337);
boolean alreadyConnected = false; // whether or not the client was connected previously


//USB
String inputString[4] = "";         // a string to hold incoming data
String TestString ="ON\n";
boolean stringComplete = false;  // whether the string is complete
boolean commandComplete = false; // whether a command is complete
int i=0;

//I2C
byte response[2]; // this data is sent to PI
volatile short LDR_value; // Global Declaration
const int LDR_pin=A0; //pin to which LDR is connected A0 is analog A0 pin 

//Preferences
bool IC2 = false;
bool USB = false;
bool TCPServer = true;
char inChar;
long OnTime = 5;                       // milliseconds of on-time
long OffTime = 5;                      // milliseconds of off-time
unsigned long previousMillis = 0;        // will store last time LED was updated
unsigned long previousMillis2 = 0;        // will store last time LED was updated
int ledState = LOW;                      // ledState used to set the LED



  int ledPinR = 5;
  int ledPinG = 6;
  int ledPinB = 9;
  int oldRed=0;
  int oldGreen=0;
  int oldBlue=0;
  int newRed=0;
  int newGreen=0;
  int newBlue=0;
  bool glowMode=0;
  int GlowStep=0;


// The setup() function runs once each time the micro-controller starts
void setup() 
{
  // initialize serial:
  Serial.begin(9600);
  softSerial.begin(9600);
  // reserve 200 bytes for the inputString:
  inputString[0].reserve(255);
  inputString[1].reserve(255);
  inputString[2].reserve(255);
  inputString[3].reserve(255);


  pinMode(ledPinR, OUTPUT);   // sets the pin as output
  pinMode(ledPinG, OUTPUT);   // sets the pin as output
  pinMode(ledPinB, OUTPUT);   // sets the pin as output

  
  Wire.begin(SLAVE_ADDRESS); // this will begin I2C Connection with 0x40 address
  Wire.onRequest(sendI2CData); // sendData is funtion called when Pi requests data
  Wire.onReceive(receiveI2CDataEvent); // register event
  pinMode(LDR_pin,INPUT);
  

  if (TCPServer==true)
  {
    Serial.println("Server gestartet");
    // initialize the ethernet device
    Bridge.begin();
    //Ethernet.begin(mac, ip, myDns, gateway, subnet);
    // start listening for clients
    server.begin();
    Serial.print("Chat server address:");
   // Serial.println(Ethernet.localIP());
  }


}

// Add the main program code into the continuous loop() function
void loop() 
{

 FadeColors();
 GlowColors();
  
  ///////////////////////
  //TCP Server
  ///////////////////////
  if (TCPServer==true)
  {

    // wait for a new client:
    BridgeClient client = server.accept();
    
    // when the client sends the first byte, say hello:
    if (client) 
    {
      if (!alreadyConnected) 
      {
        // clear out the input buffer:
        client.flush();
        Serial.println("We have a new client");
        client.println("Hello, client!");
        alreadyConnected = true;

        // clear the string:
        inputString[0] = "";
        inputString[1] = "";
        inputString[2] = "";
        inputString[3] = ""; 
                
      }
  
      while (client.available()) 
      {
            
        inChar = (char)client.read();
          loopThroughInputData();
        // read the bytes incoming from the client:
        //char thisChar = client.read();
        //echo the bytes back to the client:
        //server.write(thisChar);
        // echo the bytes to the server as well:
        //Serial.write(thisChar);
        if (client.available()>1)
          client.println("Command recived");
      }
      
    }
  }
  ///////////////////////
  
  // print the string when a newline arrives:
  if (stringComplete) 
  {
    Serial.println(inputString[0]);
    Serial.println("--------");
    Serial.println(inputString[1]);
    Serial.println("--------");
//    Serial.println(inputString[2]);
//    Serial.println("--------");
//    Serial.println(inputString[3]);

    if (inputString[0].equals("IR;"))
    {
      digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
      IR_Command(inputString[1]);
      Serial.println("############");
      Serial.println("IR");
      Serial.println("############");
    }
    else if (inputString[0].equals("LIGHT;"))
    {
      measureLight();
      sendI2CData();
      Serial.println("############");
      Serial.println("Light");
      Serial.println("############");
    }
    else if (inputString[0].equals("LED;"))
    {
      Serial.println("############");
      Serial.println("LED");
      Serial.println("############");
	  ChangeLED(inputString[1], inputString[2], inputString[3]);
      
    }
    else if (inputString[0].equals("FS20;"))
    { 
       Serial.println("FS20 Command");
       Serial.println(inputString[1]);
       Serial.println(inputString[2]);
       
       fs20toggle(inputString[1].toInt());

       
//       int k = 0;
//       int mySensVals[8] = {0, 0, 0, 0, 0, 0, 0};
//       for(int i=0;i<8;i++)
//       {
//        
//         mySensVals[i]=inputString[0].substring(k,k+3).toInt();
//         k=k+3;
//       }
//      softSerial.write(mySensVals[1]);
    }
    else if (inputString[0].equals("GLOW;"))
    {
            Serial.println("GLOW Toggle");
            if(GlowStep==0)
            {
              GlowStep=1;
              Serial.println("set GlowStep 1");
            }
            else if(GlowStep==1)
            {
              GlowStep=0;
              Serial.println("set GlowStep 1");
            }
          
    }
    else
    {
      Serial.println("############");
      Serial.println("Unknown Command");
      Serial.println("############");
    }
    // clear the string:
    inputString[0] = "";
    inputString[1] = "";
    inputString[2] = "";
    inputString[3] = "";

    stringComplete = false;

    Serial.println("############");
    Serial.println("DONE");
    Serial.println("############");
  }
}

/*
  SerialEvent occurs whenever a new data comes in the
 hardware serial RX.  This routine is run between each
 time loop() runs, so using delay inside loop can delay
 response.  Multiple bytes of data may be available.
 */
void serialEvent() 
{
 if(USB=true)
  while (Serial.available()) 
  {
    // get the new byte:
    inChar = (char)Serial.read();
    // add it to the inputString:
    Serial.println(i);
    inputString[i] += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == ';')
    {
      i=i+1;
      Serial.println(inputString[i]);
      Serial.println(i);
    }
    else if (inChar == '\n') 
    {
      stringComplete = true;
      Serial.println("neue Zeile");
      i=0;
    }
  }
}


// function that executes whenever data is received from master
// this function is registered as an event, see setup()
void receiveI2CDataEvent(int howMany) 
{
  while ( Wire.available()) 
  { 
    // loop through all but the last
    // char c = Wire.read(); // receive byte as a character
    // Serial.print(c);         // print the character
    inChar = (char)Wire.read(); // receive byte as a character
    Serial.print(i);         // print the character

    inputString[i] += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == ';')
    {
      i=i+1;
      Serial.println(inputString[i]);
      Serial.println(i);
    }
    else if (inChar == '\n') 
    {
      stringComplete = true;
      Serial.println("neue Zeile");
      i=0;
    }
    
  }
}

void loopThroughInputData()
{
    Serial.print(i);         // print the character

    inputString[i] += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == ';')
    {
      i=i+1;
      Serial.println(inputString[i]);
      Serial.println(i);
    }
    else if (inChar == '\n') 
    {
      stringComplete = true;
      Serial.println("neue Zeile");
      i=0;
    }
}

void sendI2CData()
{
  LDR_value=analogRead(LDR_pin);
  // Arduino returns 10bit data but we need to convert it to 8bit 
  LDR_value=map(LDR_value,0,1023,0,255);
  response[0]=(byte)LDR_value;
  Wire.write(response,2); // return data to PI
}

void IR_Command(String IRCommandString)
{ 
  IRsend irsend;
  irsend.sendRC6(IRCommandString.toInt(),20);
}

void measureLight()
{
  int lightPin= A0; //Das Wort „eingang“ steht jetzt für den Wert „A0“ (Bezeichnung vom Analogport 0)
  int sensorWert = 0; //Variable für den Sensorwert mit 0 als Startwert
  
  sensorWert = analogRead(lightPin); //Die Spannung an dem Fotowiderstand auslesen und unter der Variable �sensorWert� abspeichern.

  if (sensorWert > 80) //Wenn der Sensorwert �ber 512 betr�gt�.
  {
    Serial.print("Hell");
    Serial.print(sensorWert);
  }

  else //andernfalls�
  {
    Serial.print("Dunkel");
    Serial.print(sensorWert);
  }

}

void measureTemp(int i)
{
  int tempPin = 1;        //the analog pin the TMP36's Vout (sense) pin is connected to
                        //the resolution is 10 mV / degree centigrade with a
                        //500 mV offset to allow for negative temperatures
  int tempReading;        // the analog reading from the sensor

  int int_temperatureC=0;
  int int_temperatureC2=0;
  
  tempReading = analogRead(tempPin);

  Serial.print("Temp reading = ");
  //Serial.print(tempReading);     // the raw analog reading

  // converting that reading to voltage, which is based off the reference voltage
  float voltage = tempReading * aref_voltage;
  voltage /= 1024.0;

  // now print out the temperature
  float temperatureC;
  temperatureC = (voltage - 0.5) * 100;                      //converting from 10 mv per degree wit 500 mV offset

//  tempArray[i] = temperatureC;

  // Serial.print(temperatureC); Serial.println(" degrees C");
  int_temperatureC = static_cast<int>(temperatureC);          //to degrees ((volatge - 500mV) times 100)
                                //  Serial.print(temperatureC); Serial.println(" degrees C");
  temperatureC = temperatureC - int_temperatureC;
  int_temperatureC2 = static_cast<int>(temperatureC * 100);          //to degrees ((volatge - 500mV) times 100)
  Serial.print(int_temperatureC); Serial.print(".");
  Serial.print(int_temperatureC2); Serial.println(" degrees C");
}


void fs20toggle(int actuator,String command){
  softSerial.write(0x02); //Präfix
  softSerial.write(0x06); //Befehlslänge
  softSerial.write(0xF1); //Einmal senden
  softSerial.write(houseCode1); //houseCode1
  softSerial.write(houseCode2); //houseCode2
  softSerial.write(actuator); //actuator
  if (command.equals("Toggle"))
	  softSerial.write(0x12); //toggle
  else if (command.equals("ON"))
	  softSerial.write(0x10); //ON
  else if (command.equals("OFF"))
	  softSerial.write(0x00); //OFF
  softSerial.write(0x01); //wird ignoriert
  delay(15);
  while (softSerial.available()) {
    int b;
    b = softSerial.read();
    Serial.print(b); 
    }
  Serial.println();
}

void ChangeLED(String Red,String Green, String Blue) 
{
	OnTime = 5;
	Serial.println(Red);
	newRed = Red.toInt();
	//analogWrite(ledPinR, inputString[1].toInt());
	Serial.println("############");
	Serial.println(Green);
	//analogWrite(ledPinG, inputString[2].toInt());
	newGreen = Green.toInt();
	Serial.println("############");
	Serial.println(Blue);
	//analogWrite(ledPinB, inputString[3].toInt());
	newBlue = Blue.toInt();
	Serial.println("############");
}

void FadeColors()
{
    //quasi multitasking
    // check to see if it's time to change the state of the LED
      unsigned long currentMillis = millis();

      if(currentMillis - previousMillis >= OnTime)
      {
        ledState = LOW;  // Turn it off
        previousMillis = currentMillis;  // Remember the time

        //////Red
        if(oldRed>newRed)
        {
          oldRed = oldRed-1; 
          analogWrite(ledPinR, oldRed);
        }
        else if(oldRed<newRed)
        {
          oldRed = oldRed+1; 
          analogWrite(ledPinR, oldRed);
        }
        //////Green
        if(oldGreen>newGreen)
        {
          oldGreen = oldGreen-1; 
          analogWrite(ledPinG, oldGreen);
        }
        else if(oldGreen<newGreen)
        {
          oldGreen = oldGreen+1; 
          analogWrite(ledPinG, oldGreen);
        }
        //////Blue
        if(oldBlue>newBlue)
        {
          oldBlue = oldBlue-1; 
          analogWrite(ledPinB, oldBlue);
        }
        else if(oldBlue<newBlue)
        {
          oldBlue = oldBlue+1; 
          analogWrite(ledPinB, oldBlue);
        }

        
      }
  
}

void GlowColors()
{
      //quasi multitasking
      // check to see if it's time to change the state of the LED
      unsigned long currentMillis2 = millis();

      if(currentMillis2 - previousMillis2 >= 5000)
      {
        previousMillis2 = currentMillis2;  // Remember the time

        switch (GlowStep)
        {
          case 1:
              OnTime=200;
          
              if(newRed <= 50)
              {
                newRed = 0;
              }
              else
              {
                newRed = newRed - 50;
              }
              
              if(newGreen <= 50)
              {
                newGreen = 0;
              }
              else
              {
                newGreen = newGreen - 50;
              }
              
              if(newBlue <= 50)
              {
                newBlue = 0;
              }
              else
              {
                newBlue = newBlue - 50;
              }
              GlowStep=2;
              Serial.print(GlowStep); 
              Serial.print("/n");   
              break;
           
           case 2:

              OnTime=200;
           
              if(newRed <= 205)
              {
                newRed = newRed + 50;
              }
              if(newGreen <= 205)
              {
                newGreen = newGreen + 50;
              }
              if(newBlue <= 205)
              {
                newBlue = newBlue + 50;
              }
              GlowStep=1;
              Serial.print(GlowStep); 
              Serial.print("/n");
              break;
              
           default:
              OnTime=5;
              Serial.print(GlowStep); 
              Serial.print("/n");
              break;
        }  
      }
      
}


