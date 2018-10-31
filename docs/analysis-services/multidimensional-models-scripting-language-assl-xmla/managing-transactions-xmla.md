---
title: "Managing Transactions (XMLA) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Managing Transactions (XMLA)
  Every XML for Analysis (XMLA) command sent to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] runs within the context of a transaction on the current implicit or explicit session. To manage each of these transactions, you use the [BeginTransaction](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/begintransaction-element-xmla), [CommitTransaction](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/committransaction-element-xmla), and [RollbackTransaction](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/rollbacktransaction-element-xmla) commands. By using these commands, you can create implicit or explicit transactions, change the transaction reference count, as well as start, commit, or roll back transactions.  
  
## Implicit and Explicit Transactions  
 A transaction is either implicit or explicit:  
  
 **Implicit transaction**  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates an *implicit* transaction for an XMLA command if the **BeginTransaction** command does not specify the start of a transaction. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] always commits an implicit transaction if the command succeeds, and rolls back an implicit transaction if the command fails.  
  
 **Explicit transaction**  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates an *explicit* transaction if the **BeginTransaction** command starts of a transaction. However, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] only commits an explicit transaction if a **CommitTransaction** command is sent, and rolls back an explicit transaction if a **RollbackTransaction** command is sent.  
  
 In addition, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] rolls back both implicit and explicit transactions if the current session ends before the active transaction completes.  
  
## Transactions and Reference Counts  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] maintains a transaction reference count for each session. However, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] does not support nested transactions in that only one active transaction is maintained per session. If the current session does not have an active transaction, the transaction reference count is set to zero.  
  
 In other words, each **BeginTransaction** command increments the reference count by one, while each **CommitTransaction** command decrements the reference count by one. If a **CommitTransaction** command sets the transaction count to zero, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] commits the transaction.  
  
 However, the **RollbackTransaction** command rolls back the active transaction regardless of the current value of the transaction reference count. In other words, a single **RollbackTransaction** command rolls back the active transaction, no matter how many **BeginTransaction** commands or **CommitTransaction** commands were sent, and sets the transaction reference count to zero.  
  
## Beginning a Transaction  
 The **BeginTransaction** command begins an explicit transaction on the current session and increments the transaction reference count for the current session by one. All subsequent commands are considered to be within the active transaction, until either enough **CommitTransaction** commands are sent to commit the active transaction or a single **RollbackTransaction** command is sent to roll back the active transaction.  
  
## Committing a Transaction  
 The **CommitTransaction** command commits the results of commands that are run after the **BeginTransaction** command was run on the current session. Each **CommitTransaction** command decrements the reference count for active transactions on a session. If a **CommitTransaction** command sets the reference count to zero, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] commits the active transaction. If there is no active transaction (in other words, the transaction reference count for the current session is already set to zero), a **CommitTransaction** command results in an error.  
  
## Rolling Back a Transaction  
 The **RollbackTransaction** command rolls back the results of commands that are run after the **BeginTransaction** command was run on the current session. The **RollbackTransaction** command rolls back the active transaction, regardless of the current transaction reference count, and sets the transaction reference count to zero. If there is no active transaction (in other words, the transaction reference count for the current session is already set to zero), a **RollbackTransaction** command results in an error.  
  
## See Also  
 [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  
