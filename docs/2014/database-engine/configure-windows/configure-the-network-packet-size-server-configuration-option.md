---
title: "Configure the network packet size Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "default packet size"
  - "size [SQL Server], packets"
  - "packets [SQL Server], size"
  - "network packet size option"
ms.assetid: 236985bf-fc4a-4a57-98f7-a71ef977fd7b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure the network packet size Server Configuration Option
  This topic describes how to configure the `network packet size` server configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The `network packet size` option sets the packet size (in bytes) that is used across the whole network. Packets are the fixed-size chunks of data that transfer requests and results between clients and servers. The default packet size is 4,096 bytes.  
  
> [!NOTE]  
>  Do not change the packet size unless you are certain that it will improve performance. For most applications, the default packet size is best.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the network packet size option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the network packet size option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The maximum network packet size for encrypted connections is 16,383 bytes.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technician.  
  
-   If an application does bulk copy operations or sends or receives large amounts of text or image data, a packet size larger than the default might improve efficiency because it results in fewer network read-and-write operations. If an application sends and receives small amounts of information, the packet size can be set to 512 bytes, which is sufficient for most data transfers.  
  
-   On systems that are using different network protocols, set network packet size to the size for the most common protocol used. The network packet size option improves network performance when network protocols support larger packets. Client applications can override this value.  
  
-   You can also call OLE DB, Open Database Connectivity (ODBC), and DB-Library functions request a change the packet size. If the server cannot support the requested packet size, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will send a warning message to the client. In some circumstances, changing the packet size might lead to a communication link failure, such as the following:  
  
     `Native Error: 233, no process is on the other end of the pipe.`  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the network packet size option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Advanced** node.  
  
3.  Under **Network**, select a value for the **Network Packet Size** box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the network packet size option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql) to set the value of the `network packet size` option to `6500` bytes.  
  
```tsql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE ;  
GO  
EXEC sp_configure 'network packet size', 6500 ;  
GO  
RECONFIGURE;  
GO  
  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the network packet size option  
 The setting takes effect immediately without restarting the server.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
