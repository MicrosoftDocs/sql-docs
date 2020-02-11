---
title: "Replication Agent Executables Concepts | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: "reference"
helpviewer_keywords: 
  - "programming interfaces [SQL Server replication]"
  - "programming [SQL Server replication], agents"
  - "replication [SQL Server], agents and profiles"
  - "agents [SQL Server replication], executables"
  - "command prompt [SQL Server replication]"
ms.assetid: cba476df-d4ea-44c9-bb86-81488971e328
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Replication Agent Executables Concepts
  Replication agents can be controlled programmatically in the following ways:  
  
-   Using the managed agent programming interfaces in the <xref:Microsoft.SqlServer.Replication> Namespace.  
  
-   Invoking agent executable files from the command prompt with a supplied set of parameters.  
  
 Directly invoking replication agents from the command prompt enables agents to be programmatically accessed from command-line scripting in batch files. When an agent is invoked from the command prompt, it runs under the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows security account of the user that invoked the agent or started the batch file.  
  
 Instances of the following replication agents can be run using executable files.  
  
-   [Replication Distribution Agent](../agents/replication-distribution-agent.md)  
  
-   [Replication Log Reader Agent](../agents/replication-log-reader-agent.md)  
  
-   [Replication Merge Agent](../agents/replication-merge-agent.md)  
  
-   [Replication Queue Reader Agent](../agents/replication-queue-reader-agent.md)  
  
-   [Replication Snapshot Agent](../agents/replication-snapshot-agent.md)  
  
 When invoking replication agents, you can use performance profiles to automatically pass a defined set of parameters to the agent executable. For more information, see [Replication Agent Profiles](../agents/replication-agent-profiles.md).  
  
## Examples  
 The following examples show how to invoke replication agents from the command prompt. Replication agents can also be invoked using Replication Management Objects (RMO). For more information, see [Synchronize Subscriptions &#40;Replication&#41;](../synchronize-data.md).  
  
> [!NOTE]  
>  Line breaks in these examples were added to improve readability. In a batch file, commands must be made in a single line.  
  
### Running the Snapshot Agent  
 This example batch file invokes the Snapshot Agent from the command prompt to generate a snapshot for the **AdvWorksSalesOrdersMerge** publication.  
  
```  
REM -- Declare variables  
SET Publisher=%InstanceName%;  
SET PublicationDB=AdventureWorks2012;   
SET Publication=AdvWorksSalesOrdersMerge;   
  
REM --Start the Snapshot Agent to generate the snapshot for AdvWorksSalesOrdersMerge.  
"C:\Program Files\Microsoft SQL Server\120\COM\SNAPSHOT.EXE" -Publication %Publication%   
-Publisher %Publisher% -Distributor %Publisher% -PublisherDB %PublicationDB%   
-ReplicationType 2 -OutputVerboseLevel 1 -DistributorSecurityMode 1 ;  
  
```  
  
### Running the Distribution Agent  
 This example batch file invokes the Distribution Agent from the command prompt to continuously replicate changes from the **AdvWorksProductTran** publication to a push subscriber.  
  
```  
REM -- Declare the variables.  
SET Publisher=%instancename%;  
SET Subscriber=%instancename%;  
SET PublicationDB=AdventureWorks2012;  
SET SubscriptionDB=AdventureWorks2012Replica;   
SET Publication=AdvWorksProductsTran;  
  
REM -- Start the Distribution Agent with four subscription streams.  
REM -- The following command must be supplied without line breaks.  
"C:\Program Files\Microsoft SQL Server\120\COM\DISTRIB.EXE" -Subscriber %Subscriber%   
-SubscriberDB %SubscriptionDB% -SubscriberSecurityMode 1 -Publication %Publication%   
-Publisher %Publisher% -PublisherDB %PublicationDB% -Distributor %Publisher%   
-DistributorSecurityMode 1 -Continuous -SubscriptionType 0 -SubscriptionStreams 4 ;  
  
```  
  
### Running the Merge Agent  
 This example batch file invokes the Merge Agent from the command prompt to synchronize a pull subscription to the **AdvWorksSalesOrdersMerge** publication.  
  
```  
REM -- Declare the variables.  
SET Publisher=%instancename%;  
SET Subscriber=%instancename%;  
SET PublicationDB=AdventureWorks2012;  
SET SubscriptionDB=AdventureWorks2012Replica;   
SET Publication=AdvWorksSalesOrdersMerge;  
  
REM --Start the Merge Agent with concurrent upload and download processes.  
REM -- The following command must be supplied without line breaks.  
"C:\Program Files\Microsoft SQL Server\120\COM\REPLMERG.EXE" -Publication %Publication%    
-Publisher %Publisher%  -Subscriber  %Subscriber%  -Distributor %Publisher%    
-PublisherDB %PublicationDB%  -SubscriberDB %SubscriptionDB% -PublisherSecurityMode 1    
-OutputVerboseLevel 2  -SubscriberSecurityMode 1  -SubscriptionType 1 -DistributorSecurityMode 1    
-Validate 3  -ParallelUploadDownload 1 ;  
  
```  
  
  
