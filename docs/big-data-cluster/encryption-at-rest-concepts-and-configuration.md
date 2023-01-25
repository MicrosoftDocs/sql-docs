---
title: Encryption at rest
titleSuffix: SQL Server Big Data Clusters
description: Learn about encryption at rest on SQL Server 2019 Big Data Clusters. The encryption at rest feature provides application-level encryption.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: kr2b-contr-experiment
ms.metadata: seo-lt-2019
---

# Encryption at rest concepts and configuration guide

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Beginning with Microsoft SQL Server 2019 CU8 Big Data Clusters, the encryption at rest feature is available to provide application-level encryption to all data stored in the platform. This guide describes the concepts, architecture, and configuration for the encryption at rest feature set for Big Data Clusters.

SQL Server Big Data Clusters stores data in the following locations:

* SQL Server master instance.
* HDFS. Used by Storage pool and Spark.

There are two approaches to transparently encrypt data in SQL Server Big Data Clusters:

* Volume encryption. The Kubernetes platform supports this approach. It's a best practice for Big Data Clusters deployments. This article doesn't cover volume encryption. Consult your Kubernetes platform or appliance documentation for how to properly encrypt volumes that are used for SQL Server Big Data Clusters.
* Application level encryption. This architecture refers to the encryption of data by the application handling the data before it's written to disk. In case the volumes are exposed, an attacker wouldn't be able to restore data artifacts elsewhere, unless the destination system also has been configured with the same encryption keys.

The encryption at rest feature set of SQL Server Big Data Clusters supports the core scenario of application level encryption for the SQL Server and HDFS components.

The feature provides the following capabilities:

* System-managed encryption at rest. This capability is available in CU8+.
* User-managed encryption at rest, also know as bring your own key (BYOK). Service-managed integrations were introduced in SQL Server 2019 CU8. External key provider integrations were introduced in SQL Server 2019 CU11+.

For more information, see [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md).

## Key definitions

* SQL Server Big Data Clusters key management service (KMS)

  A controller-hosted service responsible for managing keys and certificates for encryption at rest feature set for the SQL Server Big Data Clusters. The service supports the following features:

  * Secure management and storage of keys and certificates used for encryption at rest.
  * Hadoop KMS compatibility. KMS acts as the key management service for HDFS component on Big Data Clusters.
  * SQL Server Transparent Data Encryption (TDE) certificate management.

* System-managed keys

  The Big Data Clusters KMS service manages all keys and certificates for SQL Server and HDFS

* User-defined keys

  User-defined keys to be managed by Big Data Clusters KMS, commonly known as bring your own key. SQL Server Big Data Clusters supports the custom definition of keys to be used for encryption on both SQL Server and HDFS components. The Big Data Clusters KMS manages those keys.

  > [!CAUTION]
  > SQL Server master instance inherits the SQL Server TDE feature. However, manually loading custom keys from files into pods, registering them on SQL Server, and using them for TDE is not a supported scenario. The Big Data Clusters KMS won't manage those keys. The situation can lead to your databases being unreadable. In order to use external provided keys correctly, use the "External providers" feature as described in this article.

* External providers

  External key solutions compatible with Big Data Clusters KMS are supported for encryption operation delegation. This feature is supported on SQL Server 2019 CU11+. With this feature enabled, the root key of encryption is hosted outside of the Big Data Clusters Controller.

## Encryption at rest on SQL Server Big Data Clusters

The Big Data Clusters KMS controller service provides support for system-managed keys and external provider-controlled keys to achieve data encryption at rest on both SQL Server and HDFS.

Those keys and certificates are service-managed. This article provides operational guidance on how to interact with the service.

The feature set introduces the Big Data Clusters KMS controller service to provide system-managed keys and certificates for data encryption at rest on both SQL Server and HDFS. Those keys and certificates are service-managed. This article provides operational guidance on how to interact with the service.

* SQL Server instances use the established [Transparent Data Encryption (TDE)](../relational-databases/security/encryption/transparent-data-encryption.md) functionality.
* HDFS uses native Hadoop KMS within each pod to interact with Big Data Clusters KMS on the controller. This approach enables HDFS encryption zones, which provide secure paths on HDFS.

### SQL Server instances

* A system-generated certificate is installed on SQL Server pods to be used with TDE commands. The system-managed certificate naming standard is `TDECertificate` + `timestamp`. For example, `TDECertificate2020_09_15_22_46_27`.
* Master instance Big Data Clusters provisioned databases and user databases won't be encrypted automatically. Database administrators might use the installed certificate to encrypt any database.
* Compute pool and storage pool are automatically encrypted using the system-generated certificate.
* Data pool encryption, although technically possible using T-SQL `EXECUTE AT` commands, is discouraged and unsupported at this time. Using this technique to encrypt data pool databases might not be effective and encryption might not be happening at the desired state. The approach also creates an incompatible upgrade path towards next releases.
* SQL Server key rotation is achieved using standard T-SQL administrative commands. For more information, see [SQL Server Big Data Clusters Transparent Data Encryption (TDE) at rest usage guide](encryption-at-rest-sql-server-tde.md).
* Encryption monitoring happens through existing standard SQL Server DMVs for TDE.
* Backing up and restoring a TDE enabled database into the cluster is supported.
* High availability is supported. If a database on the primary instance of SQL Server is encrypted, then all secondary replica of the database is encrypted as well.

### HDFS encryption zones

* [Active Directory integration](active-directory-prerequisites.md) is required to enable encryption zones for HDFS.
* A system-generated key is provisioned in Hadoop KMS. The key name is `securelakekey`. On CU8, the default key is 256-bit and we support 256-bit AES encryption.
* A default encryption zone is provisioned using the above system-generated key on a path named */securelake*.
* Users can create other keys and encryption zones using specific instructions provided in this guide. Users are able to choose the key size of 128, 192, or 256 during key creation.
* HDFS Encryption Zones key rotation is achieved using `azdata`. For more information, see [SQL Server Big Data Clusters HDFS Encryption Zones usage guide](encryption-at-rest-hdfs-encryption-zones.md).
* HDFS Tiering mounting on top of an encryption zone isn't supported.

## Encryption at rest administration

The following list contains the administration capabilities for encryption at rest:

* [SQL Server TDE](encryption-at-rest-sql-server-tde.md) management is performed using standard T-SQL commands.
* [HDFS Encryption Zones](encryption-at-rest-hdfs-encryption-zones.md) and HDFS key management is performed using `azdata` commands.
* The following administration features are performed using [Operational Notebooks](cluster-manage-notebooks.md):
  * HDFS key backup and recover
  * HDFS key deletion

## Configuration guide

During *new deployments of SQL Server Big Data Clusters*, CU8 onwards, *encryption at rest will be enabled and configured by default*. That means:

* Big Data Clusters KMS components are deployed in the controller and generate a default set of keys and certificates.
* SQL Server is deployed with TDE turned on and certificates are installed by the controller.
* Hadoop KMS for HDFS is configured to interact with Big Data Clusters KMS for encryption operations. HDFS encryption zones are configured and ready to use.

Requirements and default behaviors described in the previous section apply.

If performing a new deployment of SQL Server Big Data Clusters CU8 onwards or upgrading directly to CU9, no other steps are required.

### Upgrade scenarios

On existing clusters, the upgrade process won't enforce new encryption or re-encryption on user data that wasn't already encrypted. This behavior is by design and the following issues need to be considered per component:

* SQL Server

  * SQL Server master instance. The upgrade process won't affect any master instance databases and installed TDE certificates. We recommend that you back up your databases and your manually installed TDE certificates before the upgrade process. We also recommend that you store those artifacts outside the big data cluster.
  * Compute and storage pool. Those databases are system-managed, volatile, and are recreated and automatically encrypted on cluster upgrade.
  * Data pool. Upgrade doesn't affect databases in the SQL Server instances part of data pool.

* HDFS

  The upgrade process won't touch HDFS files and folders outside encryption zones.

### Upgrading to CU9 from CU8 or earlier

No other steps are required.

### Upgrading to CU8 from CU6 or earlier

> [!CAUTION]
> Before upgrading to SQL Server Big Data Clusters CU8, perform a complete backup of your data.

Encryption zones won't be configured. The Hadoop KMS component won't be configured to use Big Data Clusters KMS. In order to configure and enable HDFS encryption zones after upgrade, follow instructions in the next section.

#### Enable HDFS encryption zones after upgrade to CU8

If you upgraded your cluster to CU8 (`azdata upgrade`) and want to enable HDFS encryption zones, there are two options:

* Execution of the Azure Data Studio [Operational Notebook](cluster-manage-notebooks.md) named **SOP0128 - Enable HDFS Encryption zones in Big Data Clusters** to perform the configuration.
* Run the scripts, as described below.

Requirements:

* [Active Directory](active-directory-prerequisites.md) integrated cluster.
* [!INCLUDE[azdata](../includes/azure-data-cli-azdata.md)] configured and logged into the cluster in AD mode.

Follow this procedure to reconfigure the cluster with encryption zones support.

1. After performing the upgrade with `azdata`, save the following scripts.

   Scripts execution requirements:

   * Both scripts should be located in the same directory.
   * `kubectl` on PATH config file for Kubernetes in the folder *$HOME/.kube*.

    This script should be named *run-key-provider-patch.sh*:

    ```console
    #!/bin/bash
    
    if [[ -z "${BDC_DOMAIN}" ]]; then
      echo "BDC_DOMAIN environment variable with the domain name of the cluster is not defined."
      exit 1
    fi
    
    if [[ -z "${BDC_CLUSTER_NS}" ]]; then
      echo "BDC_CLUSTER_NS environment variable with the cluster namespace is not defined."
      exit 1
    fi
    
    kubectl get configmaps -n test
    
    diff <(kubectl get configmaps mssql-hadoop-storage-0-configmap -n $BDC_CLUSTER_NS -o json | ./updatekeyprovider.py) <(kubectl get configmaps mssql-hadoop-storage-0-configmap -n $BDC_CLUSTER_NS -o json | python -m json.tool)
    
    diff <(kubectl get configmaps mssql-hadoop-sparkhead-configmap -n $BDC_CLUSTER_NS -o json | ./updatekeyprovider.py) <(kubectl get configmaps mssql-hadoop-sparkhead-configmap -n $BDC_CLUSTER_NS -o json | python -m json.tool)
    
    # Replace the config maps.
    #
    kubectl replace -n $BDC_CLUSTER_NS -o json -f <(kubectl get configmaps mssql-hadoop-storage-0-configmap -n $BDC_CLUSTER_NS -o json | ./updatekeyprovider.py)
    
    kubectl replace -n $BDC_CLUSTER_NS -o json -f <(kubectl get configmaps mssql-hadoop-sparkhead-configmap -n $BDC_CLUSTER_NS -o json | ./updatekeyprovider.py)
    
    # Restart the pods which need to have the necessary changes with the core-site.xml
    kubectl delete pods -n $BDC_CLUSTER_NS nmnode-0-0
    kubectl delete pods -n $BDC_CLUSTER_NS storage-0-0
    kubectl delete pods -n $BDC_CLUSTER_NS storage-0-1
    
    # Wait for sometime for pods to restart
    #
    sleep 300
    
    # Check for the KMS process status.
    #
    kubectl exec -n $BDC_CLUSTER_NS -c hadoop nmnode-0-0 -- bash -c 'ps aux | grep kms'
    kubectl exec -n $BDC_CLUSTER_NS -c hadoop storage-0-0 -- bash -c 'ps aux | grep kms'
    kubectl exec -n $BDC_CLUSTER_NS -c hadoop storage-0-1 -- bash -c 'ps aux | grep kms'
    ```

    This script should be named *updatekeyprovider.py*:

    ```python
    #!/usr/bin/env python3
    
    import json
    import re
    import sys
    import xml.etree.ElementTree as ET
    import os
    
    class CommentedTreeBuilder(ET.TreeBuilder):
        def comment(self, data):
            self.start(ET.Comment, {})
            self.data(data)
            self.end(ET.Comment)
    
    domain_name = os.environ['BDC_DOMAIN']
    
    parser = ET.XMLParser(target=CommentedTreeBuilder())
    
    core_site = 'core-site.xml'
    j = json.load(sys.stdin)
    cs = j['data'][core_site]
    csxml = ET.fromstring(cs, parser=parser)
    props = [prop.find('value').text for prop in csxml.findall(
        "./property/name/..[name='hadoop.security.key.provider.path']")]
    
    kms_provider_path=''
    
    for x in range(5):
        if len(kms_provider_path) != 0:
            kms_provider_path = kms_provider_path + ';'
        kms_provider_path = kms_provider_path + 'nmnode-0-0.' + domain_name
    
    if len(props) == 0:
        prop = ET.SubElement(csxml, 'property')
        name = ET.SubElement(prop, 'name')
        name.text = 'hadoop.security.key.provider.path'
        value = ET.SubElement(prop, 'value')
        value.text = 'kms://https@' + kms_provider_path + ':9600/kms'
        cs = ET.tostring(csxml, encoding='utf-8').decode('utf-8')
    
    j['data'][core_site] = cs
    
    kms_site = 'kms-site.xml.tmpl'
    ks = j['data'][kms_site]
    
    kp_uri_regex = re.compile('(<name>hadoop.kms.key.provider.uri</name>\s*<value>\s*)(.*)(\s*</value>)', re.MULTILINE)
    
    def replace_uri(match_obj):
        key_provider_uri = 'bdc://https@hdfsvault-svc.' + domain_name
        if match_obj.group(2) == 'jceks://file@/var/run/secrets/keystores/kms/kms.jceks' or match_obj.group(2) == key_provider_uri:
            return match_obj.group(1) + key_provider_uri + match_obj.group(3)
        return match_obj.group(0)
    
    ks = kp_uri_regex.sub(replace_uri, ks)
    
    j['data'][kms_site] = ks
    print(json.dumps(j, indent=4, sort_keys=True))
    ```

    Run *run-key-provider-patch.sh* with the appropriate parameters.

### Configuration of external providers

As mentioned in previous sections, a SQL Server 2019 CU8+ Big Data Cluster deployment enables the encryption at rest functionality with system-managed keys by default.
In order to enable an external key provider to secure the root keys of encryption of SQL Server and HDFS see [External Key Providers in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](encryption-at-rest-external-provider.md).

## Next steps

To learn more about how key versions are used on [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)], see [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md).

To learn more about how to effectively use encryption at rest [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]
see the following articles:

* [Encryption at rest - SQL Server TDE](encryption-at-rest-sql-server-tde.md)
* [Encryption at rest - HDFS encryption zones](encryption-at-rest-hdfs-encryption-zones.md)

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following overview:

* [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
