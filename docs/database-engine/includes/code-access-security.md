---
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.topic: include
---
CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` might be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. In [!INCLUDE [sssql17](../../includes/sssql17-md.md)] and later versions, the `sp_configure` option, [clr strict security](../configure-windows/clr-strict-security.md), enhances the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but isn't recommended.

We recommend that you sign all assemblies by a certificate or asymmetric key, with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the `master` database. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] administrators can also add assemblies to a list of assemblies, which the Database Engine should trust. For more information, see [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md).
