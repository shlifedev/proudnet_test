﻿ 





// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
            
using Nettention.Proud; 
namespace Game.Network.S2C
{
	public class Common
	{
		// Message ID that replies to each RMI method. 
			public const Nettention.Proud.RmiID SendTest = (Nettention.Proud.RmiID)3000+1;
			public const Nettention.Proud.RmiID NotifyJoinPlayer = (Nettention.Proud.RmiID)3000+2;
			public const Nettention.Proud.RmiID NotifyLeavePlayeR = (Nettention.Proud.RmiID)3000+3;
			public const Nettention.Proud.RmiID NotifyServerMessage = (Nettention.Proud.RmiID)3000+4;
			public const Nettention.Proud.RmiID GivePlayerItem = (Nettention.Proud.RmiID)3000+5;
			public const Nettention.Proud.RmiID NotifyItemCreate = (Nettention.Proud.RmiID)3000+6;
			public const Nettention.Proud.RmiID NotifyEntityMove = (Nettention.Proud.RmiID)3000+7;
		// List that has RMI ID.
		public static Nettention.Proud.RmiID[] RmiIDList = new Nettention.Proud.RmiID[] {
			SendTest,
			NotifyJoinPlayer,
			NotifyLeavePlayeR,
			NotifyServerMessage,
			GivePlayerItem,
			NotifyItemCreate,
			NotifyEntityMove,
		};
	}
}

				 
