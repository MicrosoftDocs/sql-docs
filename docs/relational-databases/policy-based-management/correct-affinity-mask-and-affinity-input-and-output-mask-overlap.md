---
title: "Correct affinity mask & affinity IO mask overlap policy"
description: Learn how to enable a policy that checks whether an instance of SQL Server has one ore more processors that are assigned to be used with both the affinity mask and the affinity I/O mask options for Policy-Based Management in SQL Server.
author: VanMSFT
ms.author: vanto
ms.date: "12/15/2023"
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Correct affinity mask and affinity input and output mask overlap
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has one or more processors that are assigned to be used with both the affinity mask and the affinity I/O mask options. On a computer that has more than one processor, the affinity mask and the affinity I/O mask options are used to designate which CPUs are used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Enabling a CPU with both the affinity mask and the affinity I/O mask can slow performance by forcing the processor to be overused.  
  
## Best practices recommendations  
 When you specify either the affinity mask or the affinity I/O mask options, you should specify both, but only enable each CPU no more than once.  
  
 Don't enable the same CPU in both the affinity mask option and the affinity I/O mask option. The bits that correspond to each CPU should be in one of the following states:  
  
-   0 in both the affinity mask option and the affinity I/O mask option  
  
-   0 in the affinity mask option and 1 in the affinity I/O mask option  
  
-   1 in the affinity mask option and 0 in the affinity I/O mask option  
  
## For more information 
 [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)  
  
 [affinity Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity-input-output-mask-server-configuration-option.md)  
  
 [affinity64 mask Server Configuration Option](../../database-engine/configure-windows/affinity64-mask-server-configuration-option.md)  
  
 [affinity64 Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity64-input-output-mask-server-configuration-option.md)  
  
## Related content

 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
