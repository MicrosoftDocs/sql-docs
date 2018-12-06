---
title: "CLR User-Defined Aggregates | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "aggregate functions [CLR integration]"
  - "custom aggregates [CLR integration]"
  - "calculations [CLR integration]"
  - "user-defined functions [CLR integration]"
ms.assetid: bad9b7e8-5967-4afa-8dc8-6d840faf9372
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# CLR User-Defined Aggregates
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Aggregate functions perform a calculation on a set of values and return a single value. Traditionally, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has supported only built-in aggregate functions, such as **SUM** or **MAX**, that operate on a set of input scalar values and generate a single aggregate value from that set. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] integration with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework common language runtime (CLR) now allows developers to create custom aggregate functions in managed code, and to make these functions accessible to [!INCLUDE[tsql](../../includes/tsql-md.md)] or other managed code.  
  
 The following table lists the topics in this section.  
  
 [Requirements for CLR User-Defined Aggregates](../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregates-requirements.md)  
 Provides an overview of the requirements for implementing CLR user-defined aggregate functions.  
  
 [Invoking CLR User-Defined Aggregate Functions](../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregate-invoking-functions.md)  
 Explains how to invoke user-defined aggregates.  
  
  
