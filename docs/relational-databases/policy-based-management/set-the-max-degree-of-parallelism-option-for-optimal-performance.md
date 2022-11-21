---
title: "Max degree of parallelism & Policy-Based Management"
description: Describes configuring a policy to verify the value of max degree of parallelism for Policy-Based Management for SQL Server. 
ms.custom: seo-lt-2019
ms.date: 07/22/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
author: VanMSFT
ms.author: vanto
---
# Set the Max Degree of Parallelism Option for Optimal Performance
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule determines whether the max degree of parallelism (MAXDOP) option for a value greater than 8. Setting this option to a larger value often causes unwanted resource consumption and performance degradation.  
  
## Best practice recommendations  
 The max degree of parallelism (MAXDOP) configuration option controls the number of processors that are used for the execution of a query in a parallel plan. This option determines the number of threads that are used for the query plan operators that perform the work in parallel. Depending on whether SQL Server is set up on a symmetric multiprocessing (SMP) computer, a non-uniform memory access (NUMA) computer, or hyperthreading-enabled processors, you have to configure the max degree of parallelism option appropriately. 
 
 Recommendations to configure MAXDOP depend on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being used. For version specific guidelines, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#recommendations), and configure the policy to verify the value of max degree of parallelism accordingly.     
  
## Next steps

 -  [Recommendations and guidelines for the max degree of parallelism configuration option in SQL Server](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md)    
 - [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#recommendations)     
 - [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)     
