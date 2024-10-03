---
title: "Distributor Password"
description: "Distributor Password"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.configuredistributionwizard.distributorpassword.f1"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Distributor Password
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  If, on the **Publishers** page of this wizard, you enabled one or more Publishers to use this server as a remote Distributor, you must specify a password for the connection replication makes between the Publisher and the remote Distributor using the **distributor_admin** login. The same password must be entered for each Publisher that uses this remote Distributor on the **Administrative Password** page of the New Publication Wizard or the Configure Distribution Wizard. For more information on security for Distributors, see [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md).  
  
## Options  
 **Password**  
 Enter a strong password for the connection between the Publisher and the remote Distributor.  
  
 **Confirm Password**  
 Re-enter the password to confirm it was entered correctly.  
  
## Related content

- [Configure Distribution](../../relational-databases/replication/configure-distribution.md)
- [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)
