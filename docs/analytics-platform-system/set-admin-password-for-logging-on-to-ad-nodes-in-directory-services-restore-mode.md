---
title: Set Active Directory password - Analytics Platform System | Microsoft Docs
description: Set Active Directory nodes admin logon password in Directory Services Restore Mode in Analytics Platform System (APS).
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Set Admin Password for Logging on to AD Nodes in Directory Services Restore Mode (DSRM) - Analytics Platform System
Directory Services Restore Mode (DSRM) is a boot mode for repairing or recovering Active Directory Domain Services (AD DS). It is used to log on to the appliance AD nodes after AD DS has failed or when AD DS needs to be restored. The password for DSRM was initialized during the appliance setup at the hardware vendor site and should be changed by the appliance administrator. Analytics Platform System has two AD DS (domain controllers); **_appliance_domain_-AD01** and **_appliance_domain_-AD02**. For each appliance AD node, change the DSRM password using the following steps.  
  
## <a name="HowToDSRM"></a>To reset the administrator password  
  
1.  Open a Command Prompt window on an appliance AD node <strong>_appliance_domain_-AD_xx_</strong>virtual machine.  
  
2.  At the command prompt, type `ntdsutil`.  
  
3.  At the **ntdsutil** prompt, type `set dsrm password`.  
  
4.  At the **Reset Administrator Password:** prompt, type `reset password on server null`.  
  
5.  At the prompt, type the new password.  
  
6.  Repeat steps 1 - 5 above for each appliance AD virtual machine.  
  
    > [!WARNING]  
    > Analytics Platform System does not support the dollar sign character ($) in the domain administrator or local administrator passwords. A password containing a dollar sign will validate and be usable but can block upgrade and maintenance activities.  
  
> [!NOTE]  
> If the Active Directory Domain Services or the virtual machine becomes corrupted for a specific AD virtual machine, running **ReplaceVM** for the affected AD virtual machine is the recommended corrective action. Contact CSS for assistance.  
  
## See Also  
[Password Reset &#40;Analytics Platform System&#41;](password-reset.md)  
  
