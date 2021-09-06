---
title: Auto Generated Active Directory Objects Password Rotation
description: Rotate the password for an existing Big Data Clusters integrated with Active Directory
author: cloudmelon
ms.author: melqin
ms.reviewer: wiassaf
ms.date: 09/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Auto Generated Active Directory Objects Password Rotation
[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes how to rotate the password for an existing Big Data Clusters integrated with Active Directory. 

## Overview

When the big data cluster was deployed with Active Directory integration, there were the Active Directory (AD) accounts and groups that SQL Server creates during a big data cluster deployment, see [auto-generated active directory objects](active-directory-objects.md) for further information. These objects are usually found in the [provided organizational unit (OU)](active-directory-prerequisites.md#create-an-ou) in the deployment profile configurations.  

## Challenges

One of the greatest challenging for enterprise customers are security hardening, when it comes to security-sensitive customers, they usually require security reinforcement such as [setting password expiration policy](/microsoft-365/admin/manage/set-password-expiration-policy) which allows the administrator to set user passwords never expire or expire after a certain number of days.  For a big data cluster has been deployed for a while, it requires to rotate manually the password for [those auto-generated active directory objects](active-directory-objects.md). 

## Solution

To get around aforementioned challenges, this feature is enabled to rotate the auto-generated AD objects password starting from BDC CU13 release.  The following steps are required regardless the sequence:  

### Use azdata command to rotate the password 

Use the following azdata command to update the auto generated passwords:  

```bash
   azdata bdc rotate -n <clusterName> 
   ```

This initiates a control plane upgrade followed by a big data cluster upgrade. For each rotation, a target AD Credential version will be generated to identify the same rotation across multiple services or different iterations of password rotations. For each service, if it contains a generated password, a new generated password (The passwords are 32 characters long, it contains at least 1 uppercase, 1 lowercase, and 1 digit,  and special character is not guaranteed ) will be generated will be updated in the Domain Controller. The corresponding pods will be restarted. 

>[!NOTE]
>If you have used the [App deploy feature](concept-application-deployment.md) of SQL Server BDC and have apps up and running on your current BDC cluster, you'll need to reploy the apps after the password rotation. 

### Rotating password for the BDC domain service account (DSA) 

The following notebook is aiming to update the [DSA password](active-directory-prerequisites.md#ad-account-for-bdc-domain-service-account) for SQL Server Big Data Clusters: PASS001 - Update Administrator Domain Controller Password. It’s located with other cluster management notebooks [here](cluster-manage-notebooks.md). The customer can manually update the DSA password as Big Data Clusters do not manage it. Once they do, they can provide the DSA admin username and password as environment variable parameters to the notebook. 


>[!IMPORTANT]
>**Note** that:  The rotation such as upgrade can take some time to complete depending on network speed, number of pods and more. A password rotation is separate process and cannot be done in parallel with [the cluster upgrade operation](deployment-upgrade.md) and DSA password rotation.  
>



## Next steps

- [Deploy SQL Server Big Data Clusters in Active Directory mode](active-directory-deploy.md)
- [Manage big data cluster access in Active Directory mode](active-directory-objects.md)
