---
title: "Sample: Creating a SQL Server Agent Alert with the WMI Provider | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Agent [WMI]"
  - "WMI Provider for Server Events, samples"
  - "sample applications [WMI]"
ms.assetid: d44811c7-cd46-4017-b284-c863ca088e8f
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Sample: Creating a SQL Server Agent Alert with the WMI Provider
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  One common way to use the WMI Event Provider is to create SQL Server Agent alerts that respond to specific events. The following sample presents a simple alert that saves XML deadlock graph events in a table for later analysis. SQL Server Agent submits a WQL request, receives WMI events, and runs a job in response to the event. Notice that, although several Service Broker objects are involved in processing the notification message, the WMI Event Provider handles the details of creating and managing these objects.  
  
## Example  
 First, a table is created in the `AdventureWorks` database to hold the deadlock graph event. The table contains two columns: The `AlertTime` column holds the time that the alert runs, and the `DeadlockGraph` column holds the XML document that contains the deadlock graph.  
  
 Then, the alert is created. The script first creates the job that the alert will run, adds a job step to the job, and targets the job to the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The script then creates the alert.  
  
 The job step retrieves the **TextData** property of the WMI event instance and inserts that value into the **DeadlockGraph** column of the **DeadlockEvents** table. Notice that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implicitly converts the string into XML format. Because the job step uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] subsystem, the job step does not specify a proxy.  
  
 The alert runs the job whenever a deadlock graph trace event would be logged. For a WMI alert, SQL Server Agent creates a notification query using the namespace and WQL statement specified. For this alert, SQL Server Agent monitors the default instance on the local computer. The WQL statement requests any `DEADLOCK_GRAPH` event in the default instance. To change the instance that the alert monitors, substitute the instance name for `MSSQLSERVER` in the `@wmi_namespace` for the alert.  
  
> [!NOTE]  
>  For SQL Server Agent to receive WMI events, [!INCLUDE[ssSB](../../includes/sssb-md.md)] must be enabled in **msdb** and [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
```  
USE AdventureWorks ;  
GO  
  
IF OBJECT_ID('DeadlockEvents', 'U') IS NOT NULL  
BEGIN  
    DROP TABLE DeadlockEvents ;  
END ;  
GO  
  
CREATE TABLE DeadlockEvents  
    (AlertTime DATETIME, DeadlockGraph XML) ;  
GO  
-- Add a job for the alert to run.  
  
EXEC  msdb.dbo.sp_add_job @job_name=N'Capture Deadlock Graph',   
    @enabled=1,   
    @description=N'Job for responding to DEADLOCK_GRAPH events' ;  
GO  
  
-- Add a jobstep that inserts the current time and the deadlock graph into  
-- the DeadlockEvents table.  
  
EXEC msdb.dbo.sp_add_jobstep  
    @job_name = N'Capture Deadlock Graph',  
    @step_name=N'Insert graph into LogEvents',  
    @step_id=1,   
    @on_success_action=1,   
    @on_fail_action=2,   
    @subsystem=N'TSQL',   
    @command= N'INSERT INTO DeadlockEvents  
                (AlertTime, DeadlockGraph)  
                VALUES (getdate(), N''$(ESCAPE_SQUOTE(WMI(TextData)))'')',  
    @database_name=N'AdventureWorks' ;  
GO  
  
-- Set the job server for the job to the current instance of SQL Server.  
  
EXEC msdb.dbo.sp_add_jobserver @job_name = N'Capture Deadlock Graph' ;  
GO  
  
-- Add an alert that responds to all DEADLOCK_GRAPH events for  
-- the default instance. To monitor deadlocks for a different instance,  
-- change MSSQLSERVER to the name of the instance.  
  
EXEC msdb.dbo.sp_add_alert @name=N'Respond to DEADLOCK_GRAPH',   
@wmi_namespace=N'\\.\root\Microsoft\SqlServer\ServerEvents\MSSQLSERVER',   
    @wmi_query=N'SELECT * FROM DEADLOCK_GRAPH',   
    @job_name='Capture Deadlock Graph' ;  
GO  
```  
  
## Testing the Sample  
 To see the job run, provoke a deadlock. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open two **SQL Query** tabs and connect both queries to the same instance. Run the following script in one of the query tabs. This script produces one result set and finishes.  
  
```  
USE AdventureWorks ;  
GO  
  
BEGIN TRANSACTION ;  
GO  
  
SELECT TOP(1) Name FROM Production.Product WITH (XLOCK) ;  
GO  
```  
  
 Run the following script in the second query tab. This script produces one result set and then blocks, waiting to acquire a lock on `Production.Product`.  
  
```  
USE AdventureWorks ;  
GO  
  
BEGIN TRANSACTION ;  
GO  
  
SELECT TOP(1) Name FROM Production.Location WITH (XLOCK) ;  
GO  
  
SELECT TOP(1) Name FROM Production.Product WITH (XLOCK) ;  
GO  
```  
  
 Run the following script in the first query tab. This script blocks, waiting to acquire a lock on `Production.Location`. After a short time-out, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will choose either this script or the script in the sample as the deadlock victim and end the transaction.  
  
```  
SELECT TOP(1) Name FROM Production.Location WITH (XLOCK) ;  
GO  
```  
  
 After provoking the deadlock, wait several moments for SQL Server Agent to activate the alert and run the job. Examine the contents of the `DeadlockEvents` table by running the following script:  
  
```  
SELECT * FROM DeadlockEvents ;  
GO  
```  
  
 The `DeadlockGraph` column should contain an XML document that shows all the properties of the deadlock graph event.  
  
## See Also  
 [WMI Provider for Server Events Concepts](../../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-concepts.md)  
  
  
