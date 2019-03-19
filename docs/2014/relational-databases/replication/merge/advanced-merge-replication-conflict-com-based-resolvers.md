---
title: "Microsoft COM-Based Resolvers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "COM-based resolvers [SQL Server replication]"
  - "custom resolvers [SQL Server replication]"
ms.assetid: a6637e4b-4e6b-40aa-bee6-39d98cc507c8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Microsoft COM-Based Resolvers
  All of the COM-based resolvers supplied with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] handle update conflicts, and where indicated, they also handle insert and delete conflicts. They all handle column tracking; most also handle row tracking. These and all other COM-based resolvers declare the types of conflict they can handle, and the Merge Agent uses the default resolver for all other conflict types.  
  
 The resolvers are installed during the installation process for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Execute the **sp_enumcustomresolvers** stored procedure to view all the conflict resolvers registered on a computer. Executing the procedure displays the description and globally unique identifier (GUID) for each resolver in a separate result set.  
  
 To specify a resolver, see [Specify a Merge Article Resolver](../publish/specify-a-merge-article-resolver.md).  
  
 The following table describes the attributes of the specific resolvers.  
  
|Name|Required input|Description|Comments|  
|----------|--------------------|-----------------|--------------|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Additive Conflict Resolver|Name of the column to be summed. It must have an arithmetic data type (such as **int**, **smallint**, **numeric**, and so on).|Conflict winner is determined from the priority value. Specified column values are set to the sum of the source and the destination column values. If one is set to NULL, they are set to the value of the other column.|Supports update conflicts, column tracking only.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Averaging Conflict Resolver|Name of the column to be averaged. It must have an arithmetic data type (such as **int**, **smallint**, **numeric**, and so on).|Conflict winner is determined from the priority value. The resulting column values are set to the average of the source and the destination column values. If one is set to NULL, they are set to the value of the other column.|Supports update conflicts, column tracking only.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] DATETIME (Earlier Wins) Conflict Resolver|Name of the column to be used to determine the conflict winner. It must have a **datetime** data type.|Column with the earlier **datetime** value determines the conflict winner. If one is set to NULL, the row containing the other is the winner.|Supports update conflicts, row, and column tracking. The column values are compared directly and an adjustment is not made for different time zones.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] DATETIME (Later Wins) Conflict Resolver|Name of the column to be used to determine the conflict winner. It must have **datetime** data type.|Column with the later **datetime** value determines the conflict winner. If one is set to NULL, the row containing the other is the winner.|Supports update conflicts, row, and column tracking.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Maximum Conflict Resolver|Name of the column to be used to determine the conflict winner. It must have an arithmetic data type (such as **int**, **smallint**, **numeric**, and so on).|Column with the larger numeric value determines the conflict winner. If one is set to NULL, the row containing the other is the winner.|Supports row and column tracking.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Minimum Conflict Resolver|Name of the column to be used to determine the conflict winner. It must have an arithmetic data type (such as **int**, **smallint**, **numeric**, and so on).|Column with the smaller numeric value determines the conflict winner. If one is set to NULL, the row containing the other is the winner.|Supports update conflicts, row and column tracking.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Merge Text Conflict Resolver|Name of the text column and delimiter, for example, `@resolver_info = '[col1][===]'`.|Conflict winner is determined from the priority value. The text columns in conflict are set to the merged value, consisting of the common prefix followed by the unique part from the Publisher, then by the delimiter, and finally by the unique part from the Subscriber.|Supports update conflicts, column tracking only.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscriber Always Wins Conflict Resolver|No inputs.|Subscriber, regardless of whether it is the source or destination, is the winner.|Supports all conflict types.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Priority Column Resolver|Name of the column to be used to determine the conflict winner. It must have an arithmetic data type (such as **int**, **smallint**, **numeric**, and so on).|Column with the larger numeric value determines the conflict winner. If one is set to NULL, the row containing the other is the winner.|Supports update conflicts, row and column tracking.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Upload Only Conflict Resolver|No inputs.|Changes uploaded to the Publisher are accepted; changes are not downloaded to the Subscriber.|Supports all conflict types.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Download Only Conflict Resolver|No inputs.|Changes uploaded to the Publisher are rejected; changes are downloaded to the Subscriber.|Supports all conflict types.|  
|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLServer Stored Procedure Resolver|Name of the stored procedure the resolver should call to handle the conflict.|Conflict resolution depends on the logic in the stored procedure you specify.|Supports update conflicts. For more information, see [Implement a Custom Conflict Resolver for a Merge Article](../implement-a-custom-conflict-resolver-for-a-merge-article.md)|  
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](advanced-merge-replication-conflict-detection-and-resolution.md)   
 [sp_enumcustomresolvers &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql)  
  
  
