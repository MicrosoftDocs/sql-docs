---
title: Deploy multiple in Active Directory mode
titleSuffix: SQL Server Big Data Cluster
description: Learn how to deploy more than one SQL Server Big Data Clusters in an Active Directory domain.
author: mihaelablendea
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 05/20/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

SQL Server 2019 CU5 introduces support for more than one SQL Server Big Data Cluster in a single Active Directory domain. For an overview of the end-to-end deployment process with Active Directory integration, see [Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](deploy-active-directory.md).

<!---Deploy multiple BDCs against the same Active Directory domain - if this is too long to fit in the existing topic, we can add a small section/note in the existing topic, and add the below as a subtopic -->

>[!NOTE]
>This capability is introduced starting with SQL Server 2019 CU5. It is only available for new deployments. It requires latest version of `azdata`. If you are using an older version of `azdata` and deploy BDC using the CU5 Docker image tag, you will get the same behavior as before CU5.

>[!NOTE]
> In addition to providing different values for the subdomain as described in this section, you must also use different port numbers for BDC endpoints when deploying multiple BDCs in the same Kubernetes cluster. These port numbers are configurable at deployment time through the [deployment configuration](deployment-custom-configuration.md) profiles.

## Technical background

Why two or more big data clusters could not be deployed in one domain until CU5? 

### Service principal Names (SPN) and DNS domain: 

The domain name provided at deployment time is used as AD DNS domain. This means the pods can connect to each other in the internal network using this DNS domain. Additionally, users connect to the BDC endpoints using this DNS domain. As a result, any Service Principal Name (SPN) created for a service within BDC is going to have the Kubernetes pod, service, or endpoint name qualified with this AD DNS domain. If a user deploys a second cluster in the domain, the SPNs being generated will have the same FQDN since the pod names as well as the DNS domain name do not differ between the two clusters. As an example, consider a case where the user’s AD DNS domain is `contoso.local`. One of the SPNs generated for master pool SQL Server in pod `master-0` will be `MSSQLSvc/master-0.contoso`.local:1433. In the second cluster the user would attempt to deploy, the pod name for master-0 is the same and the user will provide the same AD DNS domain (``contoso.local``) resulting in the same SPN string. Active Directory will forbid creation of a conflicting SPN leading to a deployment failure for the second cluster.

### Account Names

During a deployment of BDC with an Active Directory domain, multiple account principals are generated for services running inside the BDC. These are essentially AD user accounts and the names for them are currently not unique between clusters. This manifests in an attempt to create the same user account name for a particular service in BDC in two different clusters. The cluster that is being deployed second will run into a conflict in AD and cannot create their account. 

### Solution to solve the problem with SPNs and DNS domain 

Since SPNs must differ in any two clusters, the DNS domain name passed in at deployment time must be different. You can specify different DNS names using the newly introduced setting in the deployment configuration file: `subdomain`. If the subdomain differs between two clusters and internal communication can happen over this subdomain, the SPNs will include the subdomain achieving the required uniqueness.

>[!NOTE]
>The value passed through the subdomain setting is not a new AD domain, but a DNS domain that is used internally.

As an example, consider the case of a master pool SQL Server SPN again. If the subdomain is `bdc`, the previously discussed SPN will change to M`SSQLSvc/master-0.bdc.contoso.local:1433`.  

Customizing the value of the newly introduced subdomain parameter in the  active directory configuration spec is optional. By default, the BDC cluster name or namespace name will be used to compute the value of subdomain setting. When users want to override the subdomain name, they can do so using the new subdomain parameter being introduced in the active directory configuration spec.

### Solution to solve the problem regarding account names uniqueness 

In order to update the account names to a scheme that guarantees uniqueness we introduced the concept of account prefix. The account prefix is a portion of the account name that is unique between any two clusters. The remaining portion of the account name is constant for a given service. The new format of the account name will look like `<prefix>-<name>-<podId>`. 

>[!NOTE]
> Active Directory requires the names to be limited to 20 characters. 
> Big Data Clusters assign:
>
> - 2 characters for pod id
> - 4 characters for the static name in the middle
> - 2 hyphens
> - 12 characters for the account prefix.

Customizing the account name is optional. Use the `accountPrefix` parameter in the active directory configuration spec. SQL Server 2019 CU5 introduces `accountPrefix` in the configuration spec. By default, the subdomain name is used as the account prefix. If the subdomain name is longer than the 12 characters, the initial 12-characters substring of the subdomain name are used as account prefix.

The subdomain only applies to DNS. Hence the new ldap user account name is `bdc-ldap@contoso.local`. The account name would not be  not `bdc-ldap@bdc.contoso.local`.

In summary, these are the semantics of the parameters added in CU5 for multiple clusters in a domain:

#### `subdomain`

- Optional field
- Data type: string
- Definition: A unique DNS subdomain to use for this BDC cluster. This value should be different for each cluster deployed in the Active Directory domain.  
- Default value: When not provided, cluster name will be used as the default value
- Maximum length: 63 characters per label (label being each string separated by a dot).
- Remarks: The endpoint DNS names should use the subdomain in their FQDN.

#### `accountPrefix`

- Optional field
- Data type: string
- Definition: A unique prefix for AD accounts BDC cluster will generate. This value should be different for each cluster deployed in the Active Directory domain.
- Default value: When not provided, subdomain name will be used as the default value. When subdomain is not provided, cluster name will be used as the subdomain name, and hence cluster name will be inherited as accountPrefix as well. If the subdomain is provided and is a multipart name (contains one or more dots), user must provide an accountPrefix. 
- Maximum length: 12 characters 

### Impact on AD domain and DNS server 

There are no change required in the AD domain or domain controller to accommodate deploying multiple BDCs against the same Active Directory domain. The DNS subdomain will be automatically created in the DNS server when registering external endpoint DNS names. 

### Impact on setting up the deployment configuration file used for the BDC deployment 

The activeDirectory section in the control plane configuration will have two new optional parameters: `subdomain` and `accountPrefix`. You can to provide values for these settings only if you want to override the default behavior which is to use the cluster name (which is the same as namespace name) for each of them. 

Additionally, provide endpoint DNS names that include the appropriate DNS domain including the subdomain. As an example, consider the gateway endpoint. If you want to use the name `gateway` for the endpoint and register it in the DNS server automatically as part of BDC deployment, use `gateway.bdc.contoso.local` as the DNS name. If `bdc` is the subdomain and `contoso.local` is the AD DNS domain name.

#### Examples

Below is an example of active directory security configuration, in case you want to override subdomain and accountPrefix. 

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


Below is an example of endpoint spec for control plane endpoints for updated DNS names in the deployment configuration file. Similarly, all the endpoints in bdc.json configuration file need to be updated. 

  
```json
        "endpoints": [ 

            { 

                "serviceType": "NodePort", 

                "port": 30080, 

                "name": "Controller", 

                "dnsName": "control.bdc.contoso.local" 

            }, 

            { 

                "serviceType": "NodePort", 

                "port": 30777, 

                "name": "ServiceProxy", 

                "dnsName": "monitor.bdc.contoso.local" 

            } 

        ] 

  

```

## Questions

### Do you need to create separate OUs for different clusters? 

It is not required but recommended. Providing separate OUs for separate clusters will help you manage the generated user accounts. 

### How to revert back to the pre-CU5 behavior?

There might be scenarios where you can't accommodate the newly introduced `subdomain` parameter.

For example you must deploy a pre-CU5 release and you already upgraded `azdata` CLI, or you are deploying a new cluster using the latest CU5 release but can't modify an already existing application connection string or the DNS names. This is highly unlikely but if you need to revert to the pre-CU5 behavior you can set `useSubdomain` parameter to `false` in the active directory section of `control.json`.

The following example sets `useSubdomain` to `false` for this scenario.

```console
azdata bdc config replace -c custom-prod-kubeadm/control.json -j "$.security.activeDirectory.useSubdomain=false" 
```