---
title: Multiple SQL Server Big Data Clusters
description: Learn how to deploy multiple SQL Server Big Data Clusters in a single Active Directory domain.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 06/22/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: kr2b-contr-experiment
---

# Deploy multiple [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] within the same Active Directory domain

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article explains updates to SQL Server 2019 *Cumulative Update 5 (CU5)*, which enables configuration of multiple SQL Server 2019 Big Data Clusters. Various Big Data Clusters can now be deployed and integrated with the same Active Directory Domain.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Prior to SQL Server 2019 CU5 two issues prevented deployment of multiple big data clusters in AD domains.

- A naming conflict for service principal names and DNS domain
- Domain account principal names

## What are object name collisions?

### Service principal names (SPN) and DNS domain naming conflict

The domain name provided at deployment is used as your *Active Directory (AD)* DNS domain. This means that *pods* can connect to each other within the internal network using this DNS domain. Users also connect to big data cluster endpoints using this DNS domain. As a result, any *Service Principal Name (SPN)* created for a service within the big data cluster has the *Kubernetes* pod, service, or endpoint name qualified with this AD DNS domain. If a user deploys a second cluster in the domain, SPNs generated have the same FQDN, since pod names as well as DNS domain names don't differ between clusters. Consider a case where the AD DNS domain is `contoso.local`. One of the SPNs generated for the master pool SQL Server in pod `master-0` is `MSSQLSvc/master-0.contoso.local:1433`. In the second cluster the user attempts to deploy, the pod name for `master-0` is the same. The user provides the same AD DNS domain (``contoso.local``) resulting in the same SPN string. Active Directory forbids creation of a conflicting SPN leading to a deployment failure for the second cluster.

### Domain account principal names

During deployment of a big data cluster with an Active Directory domain, multiple account principals are generated for services running inside the big data cluster. These are essentially AD user accounts. Prior to SQL Server 2019 CU5 the names for these accounts weren't unique between clusters. This manifests in an attempt to create the same user account name for a particular service in the big data cluster in two different clusters. The second cluster being deployed experiences a conflict in AD and the account can't be created.

## How to resolve name collisions

### Steps to solve SPN and DNS domain problems - SQL Server 2019 CU5

 SPNs must be unique in clusters. The DNS domain name passed in at deployment time must also be different. You can specify different DNS names with a newly-introduced setting in the deployment configuration file: `subdomain`. If the subdomain differs between two clusters and internal communication can happen over this subdomain, the SPNs will include the subdomain achieving the required uniqueness.

>[!NOTE]
>The value passed through the subdomain setting is not a new AD domain, but a DNS domain that is used internally.

Let's return to the case of the master pool SQL Server SPN. If the subdomain is `bdc`, the discussed SPN is changed to `MSSQLSvc/master-0.bdc.contoso.local:1433`.  

The big data cluster name or namespace name is used to compute the value of subdomain settings. You can optionally customize the value of the newly introduced subdomain parameter in the active directory configuration spec. When users want to override the subdomain name, they can do so using the new subdomain parameter in the active directory configuration spec.

### How to ensure account name uniqueness

To update account names and guarantee uniqueness use account prefixes. The account prefix is a segment of the account name that is unique between any two clusters. The remaining segment of the account name can be constant. The new account name format looks like the following: `<prefix>-<name>-<podId>`.

>[!NOTE]
>Active Directory requires account names to be limited to 20 characters. The big data cluster needs to use 8 of the characters for distinguishing pods and StatefulSets. This leaves 12 characters for the account prefix.

You have the option to customize your account name. Use the `accountPrefix` parameter in the active directory configuration spec. SQL Server 2019 CU5 introduces `accountPrefix` in the configuration spec. By default, the subdomain name is used as the account prefix. If the subdomain name is longer than the 12 characters, the initial 12-characters substring of the subdomain name is used as an account prefix.

The subdomain only applies to the DNS. Hence the new LDAP user account name is `bdc-ldap@contoso.local`. The account name is not `bdc-ldap@bdc.contoso.local`.

## Semantics

The following are parameters added in SQL Server 2019 CU5 for configuring multiple clusters in a domain:

### `subdomain`

- Optional field
- Data type: string
- Definition: A unique DNS subdomain to use for this big data cluster. This value should be different for each cluster deployed in the Active Directory domain.  
- Default value: When not provided, cluster name will be used as the default value
- Maximum length: 63 characters per label (label being each string separated by a dot).
- Remarks: The endpoint DNS names should use the subdomain in their FQDN.

### `accountPrefix`

- Optional field
- Data type: string
- Definition: A unique prefix for AD accounts that big data cluster will generate. This value should be different for each cluster deployed in the Active Directory domain.
- Default value: When not provided, subdomain name will be used as the default value. When subdomain is not provided, cluster name will be used as the subdomain name, and hence cluster name will be inherited as accountPrefix as well. If the subdomain is provided and is a multipart name (contains one or more dots), user must provide an accountPrefix.
- Maximum length: 12 characters

## AD domain and DNS server adjustments

There are no changes required in the AD domain or domain controller to accommodate deployment of multiple, big data clusters against the same Active Directory domain. The DNS subdomain will be automatically created in the DNS server when it registers external endpoint DNS names.

## Changes to the deployment configuration file

The *activeDirectory* section in the control plane configuration *control.json* has two new optional parameters: `subdomain` and `accountPrefix`. The cluster name is used for each of these parameters. Provide new values for these settings if you wish to override default behavior. The cluster name is the same as namespace name.

You have the option to use any endpoint DNS name, as long as it's fully qualified. It also can't conflict with any other big data cluster deployed in the same domain. You can use the value of the subdomain parameter to ensure DNS names are different across clusters. Consider the gateway endpoint. You can use the name `gateway` for the endpoint and register it in the DNS server automatically. To do this as part of your big data cluster deployment, use `gateway.bdc1.contoso.local` as the DNS name. If `bdc1` is the subdomain and `contoso.local` is the AD DNS domain name. Other acceptable values are: `gateway-bdc1.contoso.local` or simply `gateway.contoso.local`.

## Some Active Directory security configuration examples

The following is an example of active directory security configuration, in case you want to override subdomain and accountPrefix.

```json
    "security": { 
        "activeDirectory": { 
            "ouDistinguishedName":"OU=contosoou,DC=contoso,DC=local", 
            "dnsIpAddresses": [ "10.10.10.10" ], 
            "domainControllerFullyQualifiedDns": [ "contoso-win2016-dc.contoso.local" ], 
            "domainDnsName":"contoso.local", 
            "subdomain": "bdc", 
            "accountPrefix": "myprefix", 
            "clusterAdmins": [ "contosoadmins" ], 
            "clusterUsers": [ "contosousers1", "contosousers2" ] 
        } 
    } 
  
```

The following is an example of endpoint spec for control plane endpoints. You can use any values for DNS names, as long as they are unique and fully qualified:
  
```json
        "endpoints": [ 
            { 
                "serviceType": "NodePort", 
                "port": 30080, 
                "name": "Controller", 
                "dnsName": "control-bdc1.contoso.local" 
            }, 
            { 
                "serviceType": "NodePort", 
                "port": 30777, 
                "name": "ServiceProxy", 
                "dnsName": "monitor-bdc1.contoso.local" 
            } 
        ] 
  
```

## Questions

### Do you need to create separate organizational units for different clusters?

It is not required, but is recommended. Providing separate OUs for separate clusters helps you manage the generated user accounts.

### How can I revert back to pre-CU5 behavior in SQL Server 2019?

There might be scenarios where you can't accommodate the newly introduced `subdomain` parameter. For example you must deploy a pre-CU5 release and you already upgraded [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]. This is highly unlikely, but if you need to revert to the pre-CU5 behavior you can set `useSubdomain` parameter to `false` in the active directory section of `control.json`.

The following example sets `useSubdomain` to `false` for this scenario.

```console
azdata bdc config replace -c custom-prod-kubeadm/control.json -j "$.security.activeDirectory.useSubdomain=false" 
```

## Next steps

[Troubleshoot SQL Server Big Data Cluster Active Directory integration](troubleshoot-active-directory.md)
