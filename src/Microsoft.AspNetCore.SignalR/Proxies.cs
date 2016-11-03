﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Sockets;

namespace Microsoft.AspNetCore.SignalR
{
    public class UserProxy<THub> : IClientProxy
    {
        private readonly string _userId;
        private readonly HubLifetimeManager<THub> _lifetimeManager;

        public UserProxy(HubLifetimeManager<THub> lifetimeManager, string userId)
        {
            _lifetimeManager = lifetimeManager;
            _userId = userId;
        }

        public Task InvokeAsync(string method, params object[] args)
        {
            return _lifetimeManager.InvokeUserAsync(_userId, method, args);
        }
    }

    public class GroupProxy<THub> : IClientProxy
    {
        private readonly string _groupName;
        private readonly HubLifetimeManager<THub> _lifetimeManager;

        public GroupProxy(HubLifetimeManager<THub> lifetimeManager, string groupName)
        {
            _lifetimeManager = lifetimeManager;
            _groupName = groupName;
        }

        public Task InvokeAsync(string method, params object[] args)
        {
            return _lifetimeManager.InvokeGroupAsync(_groupName, method, args);
        }
    }

    public class AllClientProxy<THub> : IClientProxy
    {
        private readonly HubLifetimeManager<THub> _lifetimeManager;

        public AllClientProxy(HubLifetimeManager<THub> lifetimeManager)
        {
            _lifetimeManager = lifetimeManager;
        }

        public Task InvokeAsync(string method, params object[] args)
        {
            return _lifetimeManager.InvokeAllAsync(method, args);
        }
    }

    public class SingleClientProxy<THub> : IClientProxy
    {
        private readonly string _connectionId;
        private readonly HubLifetimeManager<THub> _lifetimeManager;


        public SingleClientProxy(HubLifetimeManager<THub> lifetimeManager, string connectionId)
        {
            _lifetimeManager = lifetimeManager;
            _connectionId = connectionId;
        }

        public Task InvokeAsync(string method, params object[] args)
        {
            return _lifetimeManager.InvokeConnectionAsync(_connectionId, method, args);
        }
    }

    public class GroupManager<THub> : IGroupManager
    {
        private readonly Connection _connection;
        private readonly HubLifetimeManager<THub> _lifetimeManager;

        public GroupManager(Connection connection, HubLifetimeManager<THub> lifetimeManager)
        {
            _connection = connection;
            _lifetimeManager = lifetimeManager;
        }

        public Task Add(string groupName)
        {
            return _lifetimeManager.AddGroupAsync(_connection, groupName);
        }

        public Task Remove(string groupName)
        {
            return _lifetimeManager.RemoveGroupAsync(_connection, groupName);
        }
    }
}
