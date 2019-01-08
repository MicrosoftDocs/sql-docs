---
title: "Enable a Remote Publisher at a Distributor (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "remote Distributors [SQL Server replication]"
  - "Publishers [SQL Server replication]"
ms.assetid: 6f8e2831-5c45-4e39-8e51-d37e2813cf3d
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Enable a Remote Publisher at a Distributor (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Enable a Publisher to use a remote Distributor on the **Publishers** page. This page is available in the Configure Distribution Wizard and the **Distributor Properties - \<Distributor>** dialog box. For more information about using the wizard and accessing the dialog box, see [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md) and [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
### To enable a Publisher in the Configure Distribution Wizard  
  
1.  On the **Publishers** page of the Configure Distribution Wizard, click **Add**.  
  
2.  Click **Add SQL Server Publisher**. For information about enabling an Oracle Publisher to use a Distributor, see [Create a Publication from an Oracle Database](../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md).  
  
3.  In the **Connect to Server** dialog box, specify connection information for the Publisher that will use the remote Distributor, and then click **Connect**.  
  
4.  On the **Distributor Password** page, in the **Password** and **Confirm password** text boxes, specify a strong password for the **distributor_admin** account, which replication uses to connect from the Publisher to the Distributor to perform administrative tasks.  
  
5.  To view and modify settings for a Publisher, click the properties button (**…**).  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To enable a Publisher in the Distributor Properties dialog box  
  
1.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click **Add**.  
  
2.  Click **Add SQL Server Publisher**. For information about enabling an Oracle Publisher to use a Distributor, see [Create a Publication from an Oracle Database](../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md).  
  
3.  In the **Connect to Server** dialog box, specify connection information for the Publisher that will use the remote Distributor, and then click **Connect**.  
  
4.  On the **Publishers** page, in the **Password** and **Confirm password** text boxes, specify a strong password for the **distributor_admin** account, which replication uses to connect from the Publisher to the Distributor to perform administrative tasks.  
  
5.  To view and modify settings for a Publisher, click the properties button (**…**).  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md)  
  
  
