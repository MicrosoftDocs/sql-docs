---
title: "Import Policies dialog box"
description: "Import Policies dialog box"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.dmf.importpolicy.f1"
---
# Import Policies dialog box

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use this dialog box to import one or more policies (and their referenced condition) that are saved as XML files, into the current [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] instance.

## Options

**Files to import**  
To import a policy from an XML file, type the path and name of the file or use the Browse (**...**) button.

**Replace duplicates with items imported**  
Select to overwrite any existing policy or condition of the same name if it already exists on this [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance. A condition with a dependent policy can't be overwritten unless the dependent policy is also being overwritten. If this option isn't selected, an existing condition that is using the same condition expression won't cause an error.

**Policy state**  
Select the state that you want for the imported policy:

- **Preserve policy state on import**

- **Enable all policies on import**

- **Disable all policies on import**

## Related content

- [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)
- [Import a Policy-Based Management Policy](import-a-policy-based-management-policy.md)
- [Export a Policy-Based Management Policy](export-a-policy-based-management-policy.md)
