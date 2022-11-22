---
title: "Validate Plan Guides After Upgrade | Microsoft Docs"
description: When you upgrade your application to a new release of SQL Server, we recommend that you re-evaluate and test plan guide definitions.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "plan guides [SQL Server], validating after upgrade"
ms.assetid: a55ebd88-6f58-454d-b1c4-991b88add522
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Validate plan guides after upgrade
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

We recommend re-evaluating and testing plan guide definitions when you upgrade your application to a new release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Performance tuning requirements and plan guide matching behavior may change. Although an invalid plan guide will not cause a query to fail, the plan is compiled without using the plan guide and may not be the best choice. After upgrading a database to a newer version of the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], we recommend that you perform the following tasks:
  
- Validate existing plan guides by using the [sys.fn_validate_plan_guide](../../relational-databases/system-functions/sys-fn-validate-plan-guide-transact-sql.md) function.  
  
- Use extended events to monitor for misguided plans for some period of time by using the [Plan Guide Unsuccessful](../../relational-databases/event-classes/plan-guide-unsuccessful-event-class.md) event.  
