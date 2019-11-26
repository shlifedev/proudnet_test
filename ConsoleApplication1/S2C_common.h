#pragma once

namespace Game {
namespace Network {
namespace S2C {


	//Message ID that replies to each RMI method. 
               
    static const ::Proud::RmiID Rmi_SendTest = (::Proud::RmiID)(3000+1);
               
    static const ::Proud::RmiID Rmi_NotifyJoinPlayer = (::Proud::RmiID)(3000+2);
               
    static const ::Proud::RmiID Rmi_GivePlayerItem = (::Proud::RmiID)(3000+3);

	// List that has RMI ID.
	extern ::Proud::RmiID g_RmiIDList[];
	// RmiID List Count
	extern int g_RmiIDListCount;

}
}
}


 
