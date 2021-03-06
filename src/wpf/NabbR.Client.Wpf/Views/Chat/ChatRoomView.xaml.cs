﻿using JabbR.Client.Models;
using NabbR.Events;
using NabbR.ViewModels;
using NabbR.ViewModels.Chat;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace NabbR.Views.Chat
{
    [View("/ChatRoom")]
    [ViewModel(typeof(ChatRoomViewModel))]
    partial class ChatRoomView
    {
        private ChatRoomViewModel chatRoom;

        public ChatRoomView()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        public ChatRoomView(IEventAggregator eventAggregator)
            : this()
        {
            eventAggregator.Subscribe(this);
        }

        public ChatRoomViewModel ChatRoom
        {
            get { return this.chatRoom; }
            private set
            {
                if (this.chatRoom != value)
                {
                    if (this.chatRoom != null) this.chatRoom.PropertyChanged -= OnChatRoomPropertyChanged;
                    this.chatRoom = value;
                    if (this.chatRoom != null) this.chatRoom.PropertyChanged += OnChatRoomPropertyChanged;
                }
            }
        }

        private void UserFilter(object sender, FilterEventArgs e)
        {
            UserViewModel userViewModel = (UserViewModel)e.Item;

            if (userViewModel.Status == UserStatus.Offline)
            {
                e.Accepted = false;
            }
        }
        private void ComposeMessageElement_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.None && (e.Key == Key.Enter || e.Key == Key.Return))
            {
                ICommand command = this.chatRoom.SendMessageCommand;

                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }

                e.Handled = true;
            }
        }
        private void OnChatRoomPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LoadingState")
            {
                String stateName = String.Concat("Chat", this.ChatRoom.LoadingState);
                VisualStateManager.GoToState(this, stateName, true);
            }
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.ChatRoom = (ChatRoomViewModel)e.NewValue;
        }
    }
}
