﻿using NabbR.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NabbR.Services
{
    public interface IJabbRContext
    {
        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        String UserId { get; }
        /// <summary>
        /// Gets the username of the authenticated user.
        /// </summary>
        String Username { get; }
        Boolean IsLoggedIn { get; }
        /// <summary>
        /// Gets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        IEnumerable<RoomViewModel> Rooms { get; }
        IEnumerable<DirectMessageRoomViewModel> DirectMessageRooms { get; }
        Task SetTyping(String roomName);
        Task<RoomViewModel> JoinRoom(String roomName);
        Task<IEnumerable<LobbyRoomViewModel>> GetLobbyRooms();
        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<Boolean> LoginAsync(String username, String password);
        Task<Boolean> SendMessage(String message, String roomName);
    }
}
