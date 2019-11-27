﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;

            
using Nettention.Proud; 
            
using System.Collections.Generic; 
namespace Game.Network.S2C
{
	public class Proxy:Nettention.Proud.RmiProxy
	{
public bool SendTest(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string value)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.SendTest;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, value);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_SendTest, Common.SendTest);
}

public bool SendTest(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, string value)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.SendTest;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, value);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_SendTest, Common.SendTest);
}
public bool NotifyJoinPlayer(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, GameServer.Struct.NEntityList joinedPlayers)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyJoinPlayer;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, joinedPlayers);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyJoinPlayer, Common.NotifyJoinPlayer);
}

public bool NotifyJoinPlayer(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, GameServer.Struct.NEntityList joinedPlayers)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyJoinPlayer;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, joinedPlayers);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyJoinPlayer, Common.NotifyJoinPlayer);
}
public bool NotifyLeavePlayeR(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int playerHostId)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyLeavePlayeR;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, playerHostId);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyLeavePlayeR, Common.NotifyLeavePlayeR);
}

public bool NotifyLeavePlayeR(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int playerHostId)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyLeavePlayeR;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, playerHostId);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyLeavePlayeR, Common.NotifyLeavePlayeR);
}
public bool NotifyServerMessage(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, string msg)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyServerMessage;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, msg);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyServerMessage, Common.NotifyServerMessage);
}

public bool NotifyServerMessage(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, string msg)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyServerMessage;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, msg);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyServerMessage, Common.NotifyServerMessage);
}
public bool NotifyItemCreate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int id, int itemIndex, UnityEngine.Vector3 createdPosition)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyItemCreate;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, id);
		MyMarshaler.Write(__msg, itemIndex);
		MyMarshaler.Write(__msg, createdPosition);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyItemCreate, Common.NotifyItemCreate);
}

public bool NotifyItemCreate(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int id, int itemIndex, UnityEngine.Vector3 createdPosition)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyItemCreate;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, id);
MyMarshaler.Write(__msg, itemIndex);
MyMarshaler.Write(__msg, createdPosition);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyItemCreate, Common.NotifyItemCreate);
}
public bool NotifyEntityMove(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int id, UnityEngine.Vector3 position)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyEntityMove;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, id);
		MyMarshaler.Write(__msg, position);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyEntityMove, Common.NotifyEntityMove);
}

public bool NotifyEntityMove(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int id, UnityEngine.Vector3 position)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyEntityMove;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, id);
MyMarshaler.Write(__msg, position);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyEntityMove, Common.NotifyEntityMove);
}
public bool NotifyPlayerCreate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int ownerHid, int entityId, UnityEngine.Vector3 createdPosition)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.NotifyPlayerCreate;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, ownerHid);
		MyMarshaler.Write(__msg, entityId);
		MyMarshaler.Write(__msg, createdPosition);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_NotifyPlayerCreate, Common.NotifyPlayerCreate);
}

public bool NotifyPlayerCreate(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int ownerHid, int entityId, UnityEngine.Vector3 createdPosition)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.NotifyPlayerCreate;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, ownerHid);
MyMarshaler.Write(__msg, entityId);
MyMarshaler.Write(__msg, createdPosition);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_NotifyPlayerCreate, Common.NotifyPlayerCreate);
}
public bool GivePlayerItem(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, HostID PlayerHostID)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.GivePlayerItem;
		__msg.Write(__msgid);
		MyMarshaler.Write(__msg, PlayerHostID);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_GivePlayerItem, Common.GivePlayerItem);
}

public bool GivePlayerItem(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, HostID PlayerHostID)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.GivePlayerItem;
__msg.Write(__msgid);
MyMarshaler.Write(__msg, PlayerHostID);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_GivePlayerItem, Common.GivePlayerItem);
}
#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_SendTest="SendTest";
public const string RmiName_NotifyJoinPlayer="NotifyJoinPlayer";
public const string RmiName_NotifyLeavePlayeR="NotifyLeavePlayeR";
public const string RmiName_NotifyServerMessage="NotifyServerMessage";
public const string RmiName_NotifyItemCreate="NotifyItemCreate";
public const string RmiName_NotifyEntityMove="NotifyEntityMove";
public const string RmiName_NotifyPlayerCreate="NotifyPlayerCreate";
public const string RmiName_GivePlayerItem="GivePlayerItem";
       
public const string RmiName_First = RmiName_SendTest;
#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_SendTest="";
public const string RmiName_NotifyJoinPlayer="";
public const string RmiName_NotifyLeavePlayeR="";
public const string RmiName_NotifyServerMessage="";
public const string RmiName_NotifyItemCreate="";
public const string RmiName_NotifyEntityMove="";
public const string RmiName_NotifyPlayerCreate="";
public const string RmiName_GivePlayerItem="";
       
public const string RmiName_First = "";
#endif
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 
	}
}

