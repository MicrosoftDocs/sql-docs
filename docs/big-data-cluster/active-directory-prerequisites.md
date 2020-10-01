---
title: Deploy in Active Directory mode - prerequisites
titleSuffix: SQL Server Big Data Cluster
description: Configure Active Directory for SQL Server Big Data Clusters
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.date: 09/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode: Prerequisites

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This document explains how to prepare to deploy a SQL Server big data cluster (BDC) in the Active Directory
authentication mode. The cluster uses an existing AD domain for authentication.

>[!Note]
>Before SQL Server 2019 CU5 release, there is a restriction in big data clusters so that only one cluster could be deployed against an Active Directory domain. This restriction is removed with the CU5 release, see [Concept: deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](active-directory-deployment-background.md) for details on the new capabilities. Examples in this article are adjusted to accommodate both deployment use cases.

## Background

To enable Active Directory (AD) authentication, the BDC automatically creates the users, groups, machine accounts, and service principal names (SPN) that the various services in the cluster need. To provide some containment of these accounts and allow scoping permissions, we suggest create an organizational unit (OU) before cluster deployment. All BDC-related AD objects will be created during deployment. 

## Pre-requisites

### Organizational Unit (OU)
An organizational unit (OU) is a subdivision within an Active Directory into where place users, groups, and even other organizational units. Big picture Organizational units can be used to mirror an organization's functional or business structure. This article we'll create an OU called `bdc` as an example. 

>[!NOTE]
>The organizational unit (OU) represents administrative boundaries and enable customers to control the scope of authority of data administrators. 

You can follow [OU Design Principles](/windows-server/identity/ad-ds/plan/reviewing-ou-design-concepts) to decide on the best structure on working with OUs within your organization.

### AD account for BDC domain service account

To be able to create all the required objects in Active Directory automatically, the BDC needs an AD account which have specific permissions to create users, groups, and machine accounts inside the provided organizational unit (OU). This article will explain how to configure the permission of this AD account. We use an AD Account call `bdcDSA` as an example in this article.

### Auto generated Active Directory objects
BDC deployment automatically generates account and group names. Each of the accounts represents a service in BDC and will be managed by BDC throughout the lifetime where BDC cluster is in use. Those accounts own the Service Principal Names (SPNs) are required by each service.  For a full list of AD auto-generated accounts, groups, and service that they managed, see [Auto generated Active Directory objects](active-directory-objects.md).

>[!IMPORTANT]
>Depending on the password expiration policy set in the Domain Controller, passwords for these accounts can expire. The default expiration policy is 42 days. There is no mechanism to rotate credentials for all accounts in BDC, so the cluster will become inoperable once the expiration period is met. To workaround this issue, update the expiration policy for the BDC service accounts to “Password never expires” in the Domain Controller. This action can be done before or after the expiration time. In the latter case, Active Directory will reactivate the expired passwords.
>
>The following image shows where to set this property in in Active Directory Users and Computers.
>
>:::image type="content" source="media/deploy-active-directory/image25.png" alt-text="Set password expiration policy":::

The steps below assume you already have an Active Directory domain controller. If you don't have a domain controller, the following [guide](https://social.technet.microsoft.com/wiki/contents/articles/37528.create-and-configure-active-directory-domain-controller-in-azure-windows-server.aspx) includes steps that can be helpful.

## Create AD objects

Do the following things before you deploy a BDC with AD integration:

1. Create an organizational unit (OU) where all BDC-related AD objects will be stored. Alternatively you can choose an existing OU upon deployment.
1. Create an AD account for BDC, or use an existing account, and provide this BDC AD account the right permissions inside the provided organizational unit (OU).

### Create a user in AD for BDC domain service account

The big data cluster requires an account with specific permissions. Before you proceed, make sure that you have an existing AD account or create a new account, which the big data cluster can use to set up the necessary objects.

To create a new user in AD, you can right-click the domain or the OU and select **New** > **User**:

![Active Directory users dialog](./media/deploy-active-directory/image12.png)

This user will be referred to as the *BDC domain service account* in this article.

### Create an OU

On the domain controller, open **Active Directory Users and Computers**. On the left panel, right-click the directory under which you want to create your OU and select **New** \> **Organizational Unit**, then follow the prompts from the wizard to create the OU. Alternatively, you can create an OU with PowerShell:

```powershell
New-ADOrganizationalUnit -Name "<name>" -Path "<Distinguished name of the directory you wish to create the OU in>"
```

The examples in this article use `bdc` for the OU name.

![Active Directory organizational unit](./media/deploy-active-directory/image13.png)

![New object - organizational unit](./media/deploy-active-directory/image14.png)

### Set permissions for an AD account

Whether you have created a new AD user or using an existing AD user, there are certain permissions the user needs to have. This account is the user account that the BDC controller will use when joining the cluster to AD.

The BDC domain service account (DSA) needs to be able to create users, groups, and computer accounts in the OU. In the following steps, we have named the BDC domain service account `bdcDSA`. You can choose any name for this account.

1. On the domain controller, open **Active Directory Users and Computers**

1. In the left panel, navigate to your domain, then the OU which `bdc` will use

1. Right-click the OU, and select **Properties**.

1. Go to the Security tab (Make sure that you have selected **Advanced Features** by right-clicking on the OU, and selecting **View**)

    ![BDC object properties](./media/deploy-active-directory/image15.png)

1. Click **Add...** and add the **bdcDSA** user

    ![Add BDC object properties](./media/deploy-active-directory/image16.png)

    ![Select object](./media/deploy-active-directory/image17.png)

1. Select the **bdcDSA** user and clear all permissions, then click **Advanced**

1. Click **Add**

    ![Click add](./media/deploy-active-directory/image18.png)

    - Click **Select a Principal**, insert **bdcDSA**, and click Ok

    - Set **Type** to **Allow**

    - Set **Applies To** to **This Object and all descendant objects**

        ![Set allow for properties](./media/deploy-active-directory/image19.png)

    - Scroll down to the bottom, and click **Clear all**

    - Scroll back to the top, and select:
       - **Read all properties**
       - **write all properties**
       - **Create Computer objects**
       - **Delete Computer objects**
       - **Create Group objects**
       - **Delete Group objects**
       - **Create User objects**
       - **Delete User objects**

    - Click **OK**

- Click **Add**

    - Click **Select a Principal**, insert **bdcDSA**, and click Ok

    - Set **Type** to **Allow**

    - Set **Applies To** to **Descendant Computer objects**

    - Scroll down to the bottom, and click **Clear all**

    - Scroll back to the top, and select **Reset password**

    - Click **OK**

- Click **Add**

    - Click **Select a Principal**, insert **bdcDSA**, and click Ok

    - Set **Type** to **Allow**

    - Set **Applies To** to **Descendant User objects**

    - Scroll down to the bottom, and click **Clear all**

    - Scroll back to the top, and select **Reset password**

    - Click **OK**

- Click **OK** twice more to close open dialog boxes

## Next steps

[Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](active-directory-deploy.md)

[Troubleshoot SQL Server Big Data Cluster Active Directory integration](troubleshoot-active-directory.md)

[Concept: deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](active-directory-deployment-background.md)
