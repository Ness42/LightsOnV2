﻿using LightsOn.ViewModels.PartialViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LightsOn.ViewModels
{
    public class MasterPageViewModel : ContentPage
    {

        public ListView ListView { get { return listView; } }

        ListView listView;

        public MasterPageViewModel()
        {
            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Home",
                //IconSource = "reminders.png",
                TargetType = typeof(HomeComingPageViewModel)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Lights",
                //IconSource = "reminders.png",
                TargetType = typeof(LightsPageViewModel)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Settings",
                //IconSource = "reminders.png",
                TargetType = typeof(SettingsPageViewModel)
            });


            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);
                    
                    return new ViewCell { View = grid };
                    

                }),
                SeparatorVisibility = SeparatorVisibility.None
                
            };

            //  IconImageSource = "hamburger.png";
            Title = "Personal Organiser";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };


        }


    }
}

