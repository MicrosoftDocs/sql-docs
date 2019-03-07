---
title: "Lesson 3: Using the dta Command Prompt Utility | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], tutorials"
ms.assetid: 30f27f4d-8852-4b12-ba62-57f63e496f1d
author: stevestein
ms.author: sstein
manager: craigg
---
# Lesson 3: Using the dta Command Prompt Utility
  The **dta** command-prompt utility offers functionality in addition to that provided by the Database Engine Tuning Advisor.  
  
 You can use your favorite XML tools to create input files for the utility by using the Database Engine Tuning Advisor XML schema. This schema is installed when you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and can be found at: C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn\schemas\sqlserver\2004\07\dta\dtaschema.xsd.  
  
 The Database Engine Tuning Advisor XML schema is also available online at [this Microsoft Web site](https://go.microsoft.com/fwlink/?linkid=43100&clcid=0x409).  
  
 The Database Engine Tuning Advisor XML schema provides more flexibility to set tuning options. For example, it enables you to perform "what-if" analysis. "What-if" analysis involves specifying a set of existing and hypothetical physical design structures for the database you want to tune, and then analyzing it with the Database Engine Tuning Advisor to find out whether this hypothetical physical design will improve query processing performance. This type of analysis provides the advantage of evaluating the new configuration without incurring the overhead of actually implementing it. If your hypothetical physical design does not provide the performance improvements you want, it is easy to change it and analyze it again until you reach the configuration that produces the results you need.  
  
 In addition, using the Database Engine Tuning Advisor XML schema and the **dta** command-prompt utility, you can incorporate Database Engine Tuning Advisor functionality into scripts and use it with other database design tools.  
  
 Using the XML input functionality of Database Engine Tuning Advisor is beyond the scope of this lesson.  
  
 This lesson provides an introduction to the basic **dta** command-prompt utility syntax, how to access help, and practice for tuning simple workloads.  
  
 It contains the following topic:  
  
-   Starting the **dta** Command Prompt Utility and Tuning a Workload  
  
## Next Task in Lesson  
 [Starting the dta Command Prompt Utility and Tuning a Workload](lesson-1-1-tuning-a-workload.md)  
  
  
