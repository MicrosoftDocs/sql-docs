---
title: "Connect to an Oracle Source Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "oraDb"
ms.assetid: 220cf555-0db2-443c-8f87-8e413f3ca731
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Connect to an Oracle Source Database
  Use the Oracle Source page to provide the information necessary to connect to the Oracle source database. The CDC instance will read the redo logs of the Oracle database you are connected to.  
  
 **Oracle Connect String**  
 Enter the Oracle connect string to the computer with the Oracle database you are using. The connect string is written in one of the following ways:  
  
 `host[:port][/service name]`  
  
 The connection string can also specify an Oracle Net connect descriptor, for example, `(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp) (HOST=erp.contoso.com) (PORT=1521)) (CONNECT_DATA=(SERVICE_NAME=orcl)))`  
  
 If using a directory server or tnsnames, the connect string can be the name of the connection.  
  
 **Oracle Log Mining Authentication**  
 To enter the credentials for the Oracle database user that is authorized for log mining, select one of the following:  
  
-   **Windows Authentication**: Select this to use the current Windows domain credentials. You can use this option only if the Oracle database is configured to work with Windows authentication.  
  
-   **Oracle Authentication**: If you select this option, you must type the **User Name** and **Password** for the user in the Oracle database you are connecting to.  
  
> [!NOTE]
>  A user must have the following privileges granted in the Oracle database to be a log-mining user.  
> 
>  -   SELECT on \<any-captured-table>  
> -   SELECT ANY TRANSACTION  
> -   EXECUTE on DBMS LOGMNR  
> -   SELECT on V$LOGMNR CONTENTS  
> -   SELECT on V$ARCHIVED LOG  
> -   SELECT on V$LOG  
> -   SELECT on V$LOGFILE  
> -   SELECT on V$DATABASE  
> -   SELECT on V$THREAD  
> -   SELECT on ALL INDEXES  
> -   SELECT on ALL OBJECTS  
> -   SELECT on DBA OBJECTS  
> -   SELECT on ALL TABLES  
> 
>  If any of these privileges cannot be granted to a V$xxx, then grant them to the V_S$xxx.  
  
 **Test Connection**  
 Click **Test Connection** to determine whether you established a connection with the remote computer that has the Oracle database. A dialog box opens to inform you whether the connection was successful.  
  
> [!IMPORTANT]  
>  Due to a known issue connection to the Oracle source database may fail if the CDC Designer is not run as an administrator. If connection fails, close and restart the CDC Designer using the **Run as administrator** option.  
  
 After you finish entering information on this page, click **Next** to [Select Oracle Tables and Columns](../../integration-services/change-data-capture/select-oracle-tables-and-columns.md).  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](../../integration-services/change-data-capture/how-to-create-the-sql-server-change-database-instance.md)   
 [Edit Instance Properties](../../integration-services/change-data-capture/edit-instance-properties.md)  
  
  
