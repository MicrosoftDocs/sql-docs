---
title: "Add or Replace a Database Mirroring Witness (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "witness [SQL Server], establishing"
  - "database mirroring [SQL Server], witness"
ms.assetid: 4b5ecffd-f025-4ab7-b69d-8958c6477127
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Add or Replace a Database Mirroring Witness (SQL Server Management Studio)
  If the database mirroring endpoints use Windows Authentication,, you can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to add or replace a witness. Adding a witness in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] also changes the operating mode to high-safety mode with automatic failover.  
  
> [!NOTE]  
>  We strongly recommend that the witness reside on a separate computer from either of the partners. The service account used by the witness must be in the same domain as the service accounts used by the principal and mirror server instances, or it must be in a trusted domain.  
  
### To Add or Replace a Witness  
  
1.  After connecting to the principal server instance, in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the principal database of the session to which you are adding or replacing a witness.  
  
3.  Right-click the database, select **Tasks**, and then click **Mirror**. This opens the **Mirroring** page of the **Database Properties** dialog box.  
  
4.  Click **Configure Security**.  
  
5.  If the **Configure Database Mirroring Security Wizard** welcome screen appears, click **Next**.  
  
6.  In the **Include Witness Server** dialog box, click **Yes**, and then click **Next**.  
  
7.  In the **Choose Servers to Configure** dialog box, the **Witness server instance** check box is automatically checked. Click **Next**.  
  
8.  On the **Principal Server Instance** dialog box, keep the existing port and endpoint. Click **Next**.  
  
9. On the **Witness Server Instance** dialog box, click **Connect**.  
  
10. In the **Connect to Server** dialog box, specify the witness server instance in the **Server name** field, and use Windows Authentication (the default). Click **Connect**.  
  
11. Once a connection is established, the listener port and database mirroring endpoint of the witness server instance are displayed on the **Witness Server Instance** dialog box. Click **Next**.  
  
12. The **Service Accounts** dialog box contains fields for the domain service accounts of the principal, mirror, and witness server instances.  
  
    -   If the server instances all use the same service account, leave the fields blank.  
  
    -   If the witness server instance uses a different service account from either of the partners, fill in the **Principal**, **Mirror**, and **Witness** fields with the account name:  
  
         *DOMAINNAME* **\\** *username*  
  
         The domain name must be in upper case.  
  
     Click **Next**.  
  
13. On the **Complete the Wizard** summary screen, optionally, verify the witness configuration, and then click **Finish**.  
  
14. On finishing, the wizard returns you to the **Database Properties** dialog box where the server network address of the witness now appears in **Witness** field. Also, **High-safety mode with automatic failover (synchronous)**, which is required with a witness, is automatically selected.  
  
     To enable the witness and change the session to high-safety mode with automatic failover, Click **OK**.  
  
## See Also  
 [Database Mirroring Witness](database-mirroring-witness.md)   
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](establish-database-mirroring-session-windows-authentication.md)   
 [Database Mirroring Witness](database-mirroring-witness.md)  
  
  
