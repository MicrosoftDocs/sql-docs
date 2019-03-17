---
title: "Execute Business Logic During Merge Synchronization | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "custom error resolution [SQL Server replication]"
  - "custom change handling [SQL Server replication]"
  - "errors [SQL Server replication], business logic handlers"
  - "merge replication business logic handlers [SQL Server replication]"
  - "conflict resolution [SQL Server replication], merge replication"
  - "business logic handlers [SQL Server replication]"
ms.assetid: 9d4da2ef-c17f-4a31-a1f6-5c3b7ca85f71
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Execute Business Logic During Merge Synchronization
  The business logic handler framework allows you to write a managed code assembly that is called during the merge synchronization process. The assembly includes business logic that can respond to a number of conditions during synchronization: data changes, conflicts, and errors. The business logic handler framework provides a simple programming model, and the data that the merge process provides to your assembly is in the form of an ADO.NET data set, so you can leverage knowledge of ADO.NET rather than learning a proprietary interface. For more information on programming business logic handlers, see:  
  
-   The application programming interface (API) reference: <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport>  
  
-   Instructions on how to implement a business logic handler: [Implement a Business Logic Handler for a Merge Article](../implement-a-business-logic-handler-for-a-merge-article.md)  
  
## Uses for Business Logic Handlers  
 The merge synchronization process can invoke business logic handlers to perform:  
  
-   Custom change handling  
  
-   Custom conflict resolution  
  
-   Custom error resolution  
  
> [!NOTE]  
>  The business logic handler you specify is executed for every row that is synchronized. Complex logic and calls to other applications or network services can impact performance.  
  
### Custom Change Handling  
 The business logic handler can be invoked during the processing of non-conflicting data changes and can perform one of three actions:  
  
-   Reject the data  
  
     This is useful for applications that do not want changes propagated to or from a given Subscriber. For example, an administrator could filter out inserts that do not belong in the Subscriber's partition, or possibly reject deletes performed at a Subscriber. As another example, an application could reject an order entered at a Subscriber because the inventory is no longer available.  
  
-   Accept the data  
  
     This is useful for applications in which it is necessary to review data changes made at either the Publisher or Subscriber before allowing them to be propagated. For example, a mid-tier application could examine new orders coming in from the field and integrate with a procurement workflow process in the mid-tier.  
  
-   Apply custom data  
  
     This is useful for applications that need to override specific data values or operations. For example, an application could transform a row delete into a special update that sets a **status** column in the row to a value of "deleted" and then tracks the identity of the client performing the delete. This might be useful for auditing or workflow purposes.  
  
### Custom Conflict Resolution  
 Merge replication provides conflict detection and resolution, allowing you to accept a default resolution strategy or choose custom resolution for conflicts. For more information, see [Advanced Merge Replication Conflict Detection and Resolution](advanced-merge-replication-conflict-detection-and-resolution.md). The business logic handler can be invoked during the processing of conflicting data changes and can perform one of two actions:  
  
-   Accept default resolution  
  
     This is useful for applications that might need to review the conflict, perform additional actions, and possibly log a custom conflict log message.  
  
-   Perform custom resolution  
  
     This is useful for applications that might need to select data values that are specific to their business logic and supply the synchronization process with this custom dataset. For example, an application could provide a new version of the winning row by combining values from the Publisher and Subscriber data sets.  
  
### Custom Error Resolution  
 Custom logic can be invoked during the propagation of changes that result in errors. The logic can perform one of two actions:  
  
-   Accept default error resolution  
  
     This is useful for applications that might need to review the error and perform additional action and possibly log a custom error log message.  
  
-   Accept custom error resolution  
  
     This is useful for applications that might need to select data values that are specific to their business logic and supply the synchronization process with this custom dataset. For example, if the replication process encounters a duplicate key violation, the business logic handler could provide a new version of the data change in which the key will no longer conflict. Changes made at the Publisher and Subscriber can then persist in the database, and the replication process doesn't have to compensate for the failed insert with a delete.  
  
## Deployment Scenarios for Business Logic Handlers  
 Business logic handlers can be deployed at:  
  
-   The Distributor. Use a push subscription so that business logic is executed at the Distributor.  
  
-   The Subscriber. Use a pull subscription so that business logic is executed at the Subscriber.  
  
-   An Internet Information Services (IIS) server if Web synchronization is used. Use a pull subscription synchronized with Web synchronization, and the business logic handler will execute at the IIS Server.  
  
## See Also  
 [Merge Replication](merge-replication.md)   
 [Subscribe to Publications](../subscribe-to-publications.md)   
 [Synchronize Data](../synchronize-data.md)   
 [Web Synchronization for Merge Replication](../web-synchronization-for-merge-replication.md)  
  
  
