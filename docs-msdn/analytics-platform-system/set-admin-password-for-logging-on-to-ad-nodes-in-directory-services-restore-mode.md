---
title: "Set Admin Password for Logging on to AD Nodes in Directory Services Restore Mode (DSRM) (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 97a9c715-2763-417d-b45c-bb0180759e47
caps.latest.revision: 20
author: BarbKess
---
# Set Admin Password for Logging on to AD Nodes in Directory Services Restore Mode (DSRM)
Directory Services Restore Mode (DSRM) is a boot mode for repairing or recovering Active Directory Domain Services (AD DS). It is used to log on to the appliance AD nodes after AD DS has failed or when AD DS needs to be restored. The password for DSRM was initialized during the appliance setup at the hardware vendor site and should be changed by the appliance administrator. Analytics Platform System has two AD DS (domain controllers); ***appliance_domain*-AD01** and ***appliance_domain*-AD02**. For each appliance AD node, change the DSRM password using the following steps.  
  
## <a name="HowToDSRM"></a>To reset the administrator password  
  
1.  Open a Command Prompt window on an appliance AD node ***appliance_domain*–AD*xx***virtual machine.  
  
2.  At the command prompt, type `ntdsutil`.  
  
3.  At the **ntdsutil** prompt, type `set dsrm password`.  
  
4.  At the **Reset Administrator Password:** prompt, type `reset password on server null`.  
  
5.  At the prompt, type the new password.  
  
6.  Repeat steps 1 – 5 above for each appliance AD virtual machine.  
  
    > [!WARNING]  
    > Analytics Platform System does not support the dollar sign character ($) in the domain administrator or local administrator passwords. A password containing a dollar sign will validate and be usable but can block upgrade and maintenance activities.  
  
> [!NOTE]  
> If the Active Directory Domain Services or the virtual machine becomes corrupted for a specific AD virtual machine, running **ReplaceVM** for the affected AD virtual machine is the recommended corrective action. Contact CSS for assistance.  
  
## See Also  
[Password Reset &#40;Analytics Platform System&#41;](password-reset.md)  
  
