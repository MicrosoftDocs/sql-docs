---
title: "Command Events Data Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Command Events event category"
ms.assetid: 7169f1e2-c6be-4d8c-b147-25719b84bc2c
author: minewiskan
ms.author: owend
manager: craigg
---
# Command Events Data Columns
  The following table lists the data columns for each event class in the **Command Events** event category.  
  
 The **Command Events** event category has the following event classes:  
  
-   [Command Begin class](#bkmk_1)  
  
-   [Command End class](#bkmk_2)  
  
 The following tables list the data columns for each of these event classes.  
  
##  <a name="bkmk_1"></a> Command Begin Class—Data Columns  
  
|Data Column|Description|  
|-----------------|-----------------|  
|ConnectionID|Contains the unique connection ID associated with the command event.|  
|TextData|Contains the text data associated with the command event.|  
|ServerName|Contains the name of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance on which the command event occurred.|  
|CurrentTime|Contains the current time of the command event.|  
|DatabaseName|Contains the name of the database in which the command is running.|  
|EventSubclass|Contains the class of event within the command event. Values are:<br /><br /> 0: Create<br />1: Alter<br />2: Delete<br />3: Process<br />4: DesignAggregations<br />5: WBInsert<br />6: WBUpdate<br />7: WBDelete<br />8: Backup<br />9: Restore<br />10: MergePartitions<br />11: Subscribe<br />12: Batch<br />13: BeginTransaction<br />14: CommitTransaction<br />15: RollbackTransaction<br />16: GetTransactionState<br />17: Cancel<br />18: Synchronize<br />19: Import80MiningModels<br />20: Attach<br />21: Detach<br />22: SetAuthContext<br />23: ImageLoad<br />24: ImageSave<br />10000: Other|  
|NTUserName|Contains the Windows user name associated with the command event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|RequestProperties|Contains the XML for Analysis (XMLA) request properties associated with the command event.|  
|SPID|Contains the server process ID (SPID) that uniquely identifies the user session that is associated with the command event. The SPID directly corresponds to the session GUID used by XMLA.|  
|StartTime|Contains the time at which the command event started, if available.|  
|SessionType|Contains the entity that caused the operation.|  
|NTDomainName|Contains the Windows domain account associated with the object permission event.|  
|ClientProcessID|Contains the unique client process ID associated with the command event.|  
  
##  <a name="bkmk_2"></a> Command End Class—Data Columns  
  
|Data Column|Description|  
|-----------------|-----------------|  
|ConnectionID|Contains the unique connection ID associated with the command event.|  
|TextData|Contains the text data associated with the command event.|  
|ServerName|Contains the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance on which the command event occurred.|  
|CurrentTime|Contains the current time of the command event. For filtering, the formats are *YYYY*-*MM*-*DD* and *YYYY*-*MM*-*DD HH*:*MM*:*SS*.|  
|DatabaseName|Contains the name of the database in which the command is running.|  
|Duration|Contains the approximate amount of time between the command begin and the command end event.|  
|EndTime|Contains the time at which the command event ended. For filtering, the formats are *YYYY*-*MM*-*DD* and *YYYY*-*MM*-*DD HH*:*MM*:*SS*.|  
|EventSubclass|Contains the class of event within the command event. Values are:<br /><br /> 0: Create<br />1: Alter<br />2: Delete<br />3: Process<br />4: DesignAggregations<br />5: WBInsert<br />6: WBUpdate<br />7: WBDelete<br />8: Backup<br />9: Restore<br />10: MergePartitions<br />11: Subscribe<br />12: Batch<br />13: BeginTransaction<br />14: CommitTransaction<br />15: RollbackTransaction<br />16: GetTransactionState<br />17: Cancel<br />18: Synchronize<br />19: Import80MiningModels<br />20: Attach<br />21: Detach<br />22: SetAuthContext<br />23: ImageLoad<br />24: ImageSave<br />10000: Other|  
|NTCanonicalUserName|Contains the Windows user name associated with the command event. The user name is in canonical form. For example, engineering.microsoft.com/software/user.|  
|NTUserName|Contains the Windows user account associated with the command event.|  
|SPID|Contains the server process ID (SPID) that uniquely identifies the user session associated with the command event. The SPID directly corresponds to the session GUID used by XMLA.|  
|StartTime|Contains the time at which the command end event started, if available.|  
|CPUTime|Contains the amount of CPU time (in milliseconds) used by the process between the time of the command begin event and the command end event.|  
|Error|Contains the error number of any error associated with the command event.|  
|Severity|Contains the severity level of an exception associated with the command event. Values are:<br /><br /> 0 = Success<br /><br /> 1 = Informational<br /><br /> 2 = Warning<br /><br /> 3 = Error|  
|Success|Contains the success or failure of the command event. Values are:<br /><br /> 0 = Failure<br /><br /> 1 = Success|  
|SessionType|Contains the entity that caused the operation associated with the command end event.|  
|NTDomainName|Contains the Windows domain account associated with the command event.|  
|ClientProcessID|Contains the unique client process ID associated with the command event.|  
  
## See Also  
 [Command Events Event Category](command-events-event-category.md)  
  
  
