---
title: "Monitor and Enforce Best Practices by Using Policy-Based Management | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 46788407-187e-4b0b-bfe4-529af8d77c60
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Monitor and Enforce Best Practices by Using Policy-Based Management
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Policy-Based Management allows you to monitor best practices for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a set of policy files you can import as best practice policies, and then evaluate the policies against a target set that includes instances, instance objects, databases, or database objects. Evaluate policies manually, set policies to evaluate a target set according to a schedule, or set policies to evaluate a target set according to an event. For more information about Policy-Based Management, see [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md).  
  
## Policy and Rules for Database Engine  
 The following table lists the policies included with the installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and information about the best practices rules each policy evaluates. The policies are stored as XML files and must be imported into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about how to import policies, see [Import a Policy-Based Management Policy](../../relational-databases/policy-based-management/import-a-policy-based-management-policy.md).  
  
|Policy name|Best practice rule|  
|-----------------|------------------------|  
|Asymmetric Key Encryption Algorithm|[Asymmetric Keys Encryption Strength](../../relational-databases/policy-based-management/asymmetric-keys-encryption-strength.md)|  
|Backup and Data File Location|[Backup Files Must Be on Separate Devices from the Database Files](https://msdn.microsoft.com/library/7039bebb-1f25-4cf3-81f1-393dfb78da12)|  
|Data and Log File Location|[Place Data and Log Files on Separate Drives](../../relational-databases/policy-based-management/place-data-and-log-files-on-separate-drives.md)|  
|Database Auto Close|[Set the AUTO_CLOSE Database Option to OFF](../../relational-databases/policy-based-management/set-the-auto-close-database-option-to-off.md)|  
|Database Auto Shrink|[Set the AUTO_SHRINK Database Option to OFF](../../relational-databases/policy-based-management/set-the-auto-shrink-database-option-to-off.md)|  
|Database Collation|[Set the Collation of User-defined Databases to Match Those of the master and model Databases](https://msdn.microsoft.com/library/c686446f-dae1-4b05-a3df-837b3422988d)|  
|Database Page Verification|[Set the PAGE_VERIFY Database Option to CHECKSUM](../../relational-databases/policy-based-management/set-the-page-verify-database-option-to-checksum.md)|  
|Database Page Status|[Check Integrity of Database with Suspect Pages](../../relational-databases/policy-based-management/check-integrity-of-database-with-suspect-pages.md)|  
|Guest Permissions|[Guest Permissions on User Databases](../../relational-databases/policy-based-management/guest-permissions-on-user-databases.md)|  
|Last Successful Backup Date|[Outdated Backup](../../relational-databases/policy-based-management/outdated-backup.md)|  
|Public Not Granted Server Permissions|[Server public Permissions](../../relational-databases/policy-based-management/server-public-permissions.md)|  
|SQL Server 64-bit Affinity Mask Overlap|[Correct Affinity Mask and Affinity Input and Output Mask Overlap](../../relational-databases/policy-based-management/correct-affinity-mask-and-affinity-input-and-output-mask-overlap.md)|  
|SQL Server Affinity Mask|[Keep the Affinity Mask Default Value](../../relational-databases/policy-based-management/keep-the-affinity-mask-default-value.md)|  
|SQL Server Blocked Process Threshold|[Increase or Disable Blocked Process Threshold](../../relational-databases/policy-based-management/increase-or-disable-blocked-process-threshold.md)|  
|SQL Server Default Trace|[Default Trace Log Files Disabled](../../relational-databases/policy-based-management/default-trace-log-files-disabled.md)|  
|SQL Server Dynamic Locks|[Keep the Locks Configuration Option Default Value](../../relational-databases/policy-based-management/keep-the-locks-configuration-option-default-value.md)|  
|SQL Server Lightweight Pooling|[Disable Lightweight Pooling](../../relational-databases/policy-based-management/disable-lightweight-pooling.md)|  
|SQL Server Login Mode|[Choose an Authentication Mode](../../relational-databases/security/choose-an-authentication-mode.md)|  
|SQL Server Max Degree of Parallelism|[Set the Max Degree of Parallelism Option for Optimal Performance](../../relational-databases/policy-based-management/set-the-max-degree-of-parallelism-option-for-optimal-performance.md)|  
|SQL Server Max Worker Threads for 32-bit SQL Server 2000|[Verify Max Worker Threads Setting](../../relational-databases/policy-based-management/verify-max-worker-threads-setting.md)|  
|SQL Server Max Worker Threads for 64-bit SQL Server 2000|[Verify Max Worker Threads Setting](../../relational-databases/policy-based-management/verify-max-worker-threads-setting.md)|  
|SQL Server Max Worker Threads for SQL Server 2005 and above|[Verify Max Worker Threads Setting](../../relational-databases/policy-based-management/verify-max-worker-threads-setting.md)|  
|SQL Server Network Packet Size|[Network Packet Size Should Not Exceed 8060 Bytes](../../relational-databases/policy-based-management/network-packet-size-should-not-exceed-8060-bytes.md)|  
|SQL Server Password Expiration|[SQL Server Login Password Expiration](../../relational-databases/policy-based-management/sql-server-login-password-expiration.md)|  
|SQL Server Password Policy|[SQL Server Login Password Strength](../../relational-databases/policy-based-management/sql-server-login-password-strength.md)|  
|Symmetric Key Encryption for User Databases|[Symmetric Keys on User Databases](../../relational-databases/policy-based-management/symmetric-keys-on-user-databases.md)|  
|Symmetric Key for master Database|[Symmetric Keys on System Databases](../../relational-databases/policy-based-management/symmetric-keys-on-system-databases.md)|  
|Symmetric Key for System Databases|[Symmetric Keys on System Databases](../../relational-databases/policy-based-management/symmetric-keys-on-system-databases.md)|  
|Trustworthy Database|[Trustworthy Bit](../../relational-databases/policy-based-management/trustworthy-bit.md)|  
|Windows Event Log Cluster Disk Resource Corruption Error|[Detect SCSI Host Adapter Issues](../../relational-databases/policy-based-management/detect-scsi-host-adapter-issues.md)|  
|Windows Event Log Device Driver Control Error|[Device Driver Control Error](../../relational-databases/policy-based-management/device-driver-control-error.md)|  
|Windows Event Log Device Not Ready Error|[Device Not Ready Error](../../relational-databases/policy-based-management/device-not-ready-error.md)|  
|Windows Event Log Failed I_O Request Error|[Detect Failed Input and Output Requests](../../relational-databases/policy-based-management/detect-failed-input-and-output-requests.md)|  
|Windows Event Log I_O Delay Warning|[Check Disk Input and Output Subsystem for IO Delay Problems](../../relational-databases/policy-based-management/check-disk-input-and-output-subsystem-for-io-delay-problems.md)|  
|Windows Event Log I_O Error During Hard Page Fault Error|[Input and Output Error During Hard Page Fault](../../relational-databases/policy-based-management/input-and-output-error-during-hard-page-fault.md)|  
|Windows Event Log Read Retry Error|[Check Disk Input-Output Subsystem for Read Retry Problems](../../relational-databases/policy-based-management/check-disk-input-output-subsystem-for-read-retry-problems.md)|  
|Windows Event Log Storage System I_O Timeout Error|[Storage System Input-Output Time-out](../../relational-databases/policy-based-management/storage-system-input-output-time-out.md)|  
|Windows Event Log System Failure Error|[Unexpected System Failures](../../relational-databases/policy-based-management/unexpected-system-failures.md)|  
  
## See Also  
 [Working with Policy-Based Management Facets](../../relational-databases/policy-based-management/working-with-policy-based-management-facets.md)  
  
  
