---
title: Create a domain admin
description: Some operations require Analytics Platform System domain administrator privileges. This explains how to create additional appliance domain administrators.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Create an APS Domain Administrator
Some operations require Analytics Platform System domain administrator privileges. This explains how to create additional appliance domain administrators.  
  
## Create a Domain Administrator  
To have sufficient permissions to configure all APS nodes, the user running the **APS Configuration Manager** (`dwconfig.exe`) must be a member of the **Domain Admins** group. To start and stop the APS services, the user must be a member of the **PdwControlNodeAccess** group.  
  
#### To add a user to the Domain Admins group  
  
1.  Log into the active AD node **(_appliance\_domain_-AD01** or **_appliance\_domain_-AD02**) using an existing appliance domain administrator account.  
  
2.  On the Start menu, click **Run**. In the **Open** box, type **dsa.msc**. Click **OK**.  
  
3.  In the **Active Directory Users and Computers** program, right-click **Users**, point to **New**, and then click **User**.  
  
4.  In the **New Object - User** dialog box, complete the description of the new user, and then click **Next**.  
  
    Complete the password dialog box, and then click **Next**.  
  
    > [!WARNING]  
    > SQL Server PDW does not support the dollar sign character ($) in the domain administrator or local administrator passwords. A password containing a dollar sign will valid and be usable but can block upgrade and maintenance activities  
  
    Confirm the new user description, and then click **Finish**.  
  
5.  In the list of users, double-click the new user to open the user properties dialog box.  
  
6.  On the **Member Of** tab, click **Add**.  
  
    Type **Domain Admins; PdwControlNodeAccess** and then click **Check Names**. Click **OK**.  
  
    This adds the new user to the **Domain Admins** group and the **PdwControlNodeAccess** group. Click **OK**.  
  
## See Also  
[Launch the Configuration Manager &#40;Analytics Platform System&#41;](launch-the-configuration-manager.md)  
  
