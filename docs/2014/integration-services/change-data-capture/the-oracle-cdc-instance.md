---
title: "The Oracle CDC Instance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: ed71e8c4-e013-4bf2-8b6c-1e833ff2a41d
author: janinezhang
ms.author: janinez
manager: craigg
---
# The Oracle CDC Instance
  The Oracle CDC Instance is a process created by the Oracle CDC Service to process changes captured from a single Oracle source database. The Oracle CDC Instance retrieves its configuration from the **cdc.xdbcdc_config** table and maintains its state in the **cdc.xdbcdc_state** table. These tables are part of the CDC database, which defines the Oracle CDC Instance. For more information about the xdbcdc database and tables see [The CDC Databases](the-oracle-cdc-service.md).  
  
 The following describes the tasks carried out by the Oracle CDC instance:  
  
-   **Handling service startup verification**: When started, the CDC instance loads its configuration from the **xdbcdc_config** table and performs a series of status verifications that ensure that the CDC instance persisted state is consistent and that it can start processing changes.  
  
-   **Preparing for change capture**: When the verification passes successfully, the Oracle CDC Instance scans all of the capture instances currently defined and prepares the Oracle LogMiner queries and other support structures required for change capture. In addition, the Oracle instance re-loads the internal capture state that was saved the last time the Oracle CDC Instance run.  
  
-   **Capturing changes from Oracle**: The Oracle CDC Instance pools changes from Oracle by means of the Oracle LogMiner facility, orders them in according to transaction commit, and then changes the time in a transaction and writes them to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] change tables in the CDC database.  
  
-   **Handling service shutdown**: The life cycle of the Oracle CDC Instance is managed by the Oracle CDC Service. When the Oracle CDC Instance is requested to shut down, it performs the following tasks:  
  
    -   Stops reading from the Oracle transaction log.  
  
    -   Stops writing completed Oracle transactions to the CDC database.  
  
    -   Waits for up to 30 seconds (if necessary) until the current transaction finishes writing to the CDC database. If more than 30 seconds pass, the writing is cancelled and transaction is rolled back (to be retried when the CDC instance is restarted).  
  
    -   In a separate thread, writes as many memory-cached records as possible to the staged transactions table for up to 30 seconds (from the oldest transaction to the newest), then updates the **xdbcdc_state** table and commits all the changes.  
  
-   **Handling configuration changes**: The Oracle CDC Instance is notified about configuration changes either from the CDC Service or by detecting a new version in the **cdc.xdbcdc_config** table. Most changes do not require the restart of the Oracle CDC Instance (for example, adding or removing capture instances). However, some changes, such as changing the Oracle connection string or access credentials do require the restart of the CDC Instance.  
  
-   **Handling recovery**: When an Oracle CDC Instance starts its internal state is restored from the **xdbcdc_state** and the **xdbcdc_staged_transactions** tables. Once the state is restored, the CDC instance proceeds as usual.  
  
## See Also  
 [Error Handling](error-handling.md)  
  
  
