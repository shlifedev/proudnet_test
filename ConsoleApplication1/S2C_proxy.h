﻿



  
// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

#pragma once


#include "S2C_common.h"

namespace Game {
namespace Network {
namespace S2C {


	class Proxy : public ::Proud::IRmiProxy
	{
	public:
	virtual bool SendTest ( ::Proud::HostID remote, ::Proud::RmiContext& rmiContext , const string & value) PN_SEALED; 
	virtual bool SendTest ( ::Proud::HostID *remotes, int remoteCount, ::Proud::RmiContext &rmiContext, const string & value)   PN_SEALED;  
	virtual bool NotifyJoinPlayer ( ::Proud::HostID remote, ::Proud::RmiContext& rmiContext , const int & JoinedEntityIndex, const HostID & PlayerHostID) PN_SEALED; 
	virtual bool NotifyJoinPlayer ( ::Proud::HostID *remotes, int remoteCount, ::Proud::RmiContext &rmiContext, const int & JoinedEntityIndex, const HostID & PlayerHostID)   PN_SEALED;  
	virtual bool GivePlayerItem ( ::Proud::HostID remote, ::Proud::RmiContext& rmiContext , const HostID & PlayerHostID) PN_SEALED; 
	virtual bool GivePlayerItem ( ::Proud::HostID *remotes, int remoteCount, ::Proud::RmiContext &rmiContext, const HostID & PlayerHostID)   PN_SEALED;  
static const PNTCHAR* RmiName_SendTest;
static const PNTCHAR* RmiName_NotifyJoinPlayer;
static const PNTCHAR* RmiName_GivePlayerItem;
static const PNTCHAR* RmiName_First;
		Proxy()
		{
			if(m_signature != 1)
				::Proud::ShowUserMisuseError(::Proud::ProxyBadSignatureErrorText);
		}

		virtual ::Proud::RmiID* GetRmiIDList() PN_OVERRIDE { return g_RmiIDList; } 
		virtual int GetRmiIDListCount() PN_OVERRIDE { return g_RmiIDListCount; }
	};

}
}
}


