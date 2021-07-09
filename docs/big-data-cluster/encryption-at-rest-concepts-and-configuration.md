---
title: Encryption at rest
titleSuffix: SQL Server Big Data Clusters
description: Learn all about encryption at rest on a SQL Server 2019 Big Data Cluster.
author: DaniBunny
ms.author: dacoelho
ms.reviewer: wiassaf
ms.metadata: seo-lt-2019
ms.date: 06/14/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Encryption at rest concepts and configuration guide

Starting with Microsoft SQL Server 2019 CU8 Big Data Clusters, the encryption at rest feature is available to provide application-level encryption to all data stored in the platform. This guide documents the concepts, architecture, and configuration for the encryption at rest feature set for Big Data Clusters.

SQL Server Big Data Clusters stores data in the following two locations:

* __SQL Server__ master instance
* __HDFS__ used by Storage pool and Spark.

To be able to transparently encrypt data in SQL Server Big Data Clusters, there are two possible approaches:

* __Volume encryption__. This approach is supported by the Kubernetes platform and is expected as a best practice for Big Data Clusters deployments. This guide does not cover volume encryption. Consult your Kubernetes platform or appliance documentation for guides on how to properly encrypt volumes that will be used for SQL Server Big Data Clusters.
* __Application level encryption__. This architecture refers to the encryption of data by the application handling the data before it is written to disk. In case the volumes are exposed, an attacker wouldn't be able to restore data artifacts elsewhere, unless the destination system also has been configured with the same encryption keys.

The Encryption at Rest feature set of SQL Server Big Data Clusters supports the core scenario of application level encryption for the SQL Server and HDFS components.

The following capabilities are provided:

* __System-managed encryption at rest__. This capability is available in CU8+.
* __User-managed encryption at rest (BYOK)__, with both service-managed (introduced in SQL Server 2019 CU8) and external key provider integrations (introduced in SQL Server 2019 CU11+).

For more information, see [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md)

## Key Definitions

### SQL Server Big Data Clusters key management service (KMS)

A Controller hosted service responsible for managing keys and certificates for the Encryption at Rest feature set for the SQL Server BDC cluster. It's a service that supports the following features:

* Secure management and storage of keys and certificates used for encryption at rest.
* Hadoop KMS compatibility. It acts as the key management service for HDFS component on BDC.
* SQL Server TDE certificate management.

We will reference the Big Data Clusters Key Management Server (KMS) service as __BDC KMS__ throughout the rest of this document. Also the term __BDC__ is used to refer to the [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)] computing platform.

### System-managed keys

The BDC KMS service will manage all keys and certificates for SQL Server and HDFS.

### User-defined keys

User-defined keys to be managed by BDC KMS, commonly known as bring your own key (BYOK). SQL Server BDC supports the custom definition of keys to be used for encryption on both SQL Server and HDFS components. Those keys will be managed by the BDC KMS.

> [!CAUTION]
   > SQL Server master instance inherits the SQL Server transparent data encryption (TDE) feature. However, manually loading custom keys from files into pods, registering them on SQL Server, and using them for TDE is not a supported scenario. The BDC KMS won't manage those keys and it can lead to your databases being unreadable. In order to use external provided keys correctly, use the "External providers" feature as described in this article.

### External providers

External key solutions compatible with BDC KMS are supported for encryption operation delegation. This feature is supported on SQL Server 2019 CU11+. With this feature enabled, the root key of encryption will be hosted outside of the BDC Controller.

## Encryption at rest on SQL Server Big Data Clusters

Read this document carefully to completely assess your scenario.

The BDC KMS controller service provides support for system-managed keys and external provider-controlled keys to achieve data encryption at rest on both SQL Server and HDFS.

Those keys and certificates are service-managed and this documentation provides operational guidance on how to interact with the service.

The feature set introduces the BDC KMS controller service to provide system-managed keys and certificates for data encryption at rest on both SQL Server and HDFS. Those keys and certificates are service-managed and this documentation provides operational guidance on how to interact with the service.

* __SQL Server__ instances leverage the established [Transparent Data Encryption (TDE)](../relational-databases/security/encryption/transparent-data-encryption.md) functionality.
* __HDFS__ uses native Hadoop KMS within each pod to interact with BDC KMS on the controller. This enables HDFS encryption zones, which provide secure paths on HDFS.

### SQL Server instances

* A system-generated certificate will be installed on SQL Server pods to be used with TDE commands. The system-managed certificate naming standard is `TDECertificate` + `timestamp`. For example, `TDECertificate2020_09_15_22_46_27`.
* Master instance BDC provisioned databases and user databases won't be encrypted automatically. DBAs may use the installed certificate to encrypt any database.
* Compute pool and storage pool will be automatically encrypted using the system-generated certificate.
* Data pool encryption, albeit technically possible using T-SQL `EXECUTE AT` commands, is discouraged and unsupported at this time. Using this technique to encrypt data pool databases might not be effective and encryption may not be happening at the desired state. It also creates an incompatible upgrade path towards next releases.
* SQL Server key rotation is achieved using standard T-SQL administrative commands. For more information, see [SQL Server Big Data Clusters transparent data encryption (TDE) at rest usage guide](encryption-at-rest-sql-server-tde.md).
* Encryption monitoring happens through existing standard SQL Server DMVs for TDE.
* It is supported to back up and restore a TDE enabled database into the cluster.
* HA is supported. If a database on the primary instance of SQL Server is encrypted, then all secondary replica of the database will be encrypted as well.

### HDFS encryption zones

* [Active Directory integration](active-directory-prerequisites.md) is required to enable the encryption zones feature for HDFS.
* A system-generated key will be provisioned in Hadoop KMS. The key name is `securelakekey`. On CU8 the default key is 256-bit and we support 256-bit AES encryption.
* A default encryption zone will be provisioned using the above system-generated key on a path named `/securelake`.
* Users can create additional keys and encryption zones using specific instructions provided in this guide. Users will be able to choose the key size of 128, 192, or 256 during key creation.
* HDFS Encryption Zones key rotation is achieved using `azdata`. For more information, see [SQL Server Big Data Clusters HDFS Encryption Zones usage guide](encryption-at-rest-hdfs-encryption-zones.md).
* It's not supported to perform HDFS Tiering mounting on top of an encryption zone.

## Encryption at rest administration

The following list contains the administration capabilities for Encryption at Rest

* [SQL Server TDE](encryption-at-rest-sql-server-tde.md) management is performed using standard T-SQL commands.
* [HDFS Encryption Zones](encryption-at-rest-hdfs-encryption-zones.md) and HDFS key management is performed using `azdata` commands.
* The following administration features are performed using [Operational Notebooks](cluster-manage-notebooks.md):
    - HDFS key backup and recover
    - HDFS key deletion

## Configuration guide

During __new deployments of SQL Server Big Data Clusters__, CU8 onwards, __encryption at rest will be enabled and configured by default__. That means:

* BDC KMS component will be deployed in the controller and will generate a default set of keys and certificates.
* SQL Server will be deployed with TDE turned on and certificates will get installed by the controller.
* Hadoop KMS (for HDFS) will be configured to interact with BDC KMS for encryption operations. HDFS encryption zones are configured and ready to use.

Requirements and default behaviors described in the previous section apply.

__If performing a new deployment of SQL Server BDC CU8 onwards or upgrading directly to CU9, no additional steps are required__.

### Upgrade scenarios

On existing clusters, the upgrade process won't enforce new encryption or re-encryption on user data that was not already encrypted. This behavior is by design and the following needs to be considered per component:

* __SQL Server__

    1. __SQL Server master instance__. The upgrade process won't affect any master instance databases and installed TDE certificates, but it is highly encouraged to back up your databases and your manually installed TDE certificates before the upgrade process. It is also advised to store those artifacts outside the SQL Server BDC cluster.
    1. __Compute and storage pool__. Those databases are system-managed, volatile and will be recreated and automatically encrypted on cluster upgrade.
    1. __Data pool__. Upgrade does not impact databases in the SQL Server instances part of data pool.

* __HDFS__

    1. __HDFS__. The upgrade process won't touch HDFS files and folders outside encryption zones.

### Upgrading to CU9 from CU8 or earlier

No additional steps are required.

### Upgrading to CU8 from CU6 or earlier

   > [!CAUTION]
   > Before upgrading to SQL Server Big Data Clusters CU8 perform a complete backup of your data.


__Encryption Zones won't be configured__. The Hadoop KMS component won't be configured to use BDC KMS. In order to configure and enable HDFS encryption zones feature after upgrade follow instructions of the next section.

#### Enable HDFS encryption zones after upgrade to CU8

If you upgraded your cluster to CU8 (`azdata upgrade`) and want to enable HDFS encryption zones there are two options available:

* Execution of the Azure Data Studio [Operational Notebook](cluster-manage-notebooks.md) named __SOP0128 - Enable HDFS Encryption zones in Big Data Clusters__ to perform the configuration.
* Script execution as described bellow.

Requirements:

- [Active Directory](active-directory-prerequisites.md) integrated cluster.

- [!INCLUDE[azdata](../includes/azure-data-cli-azdata.md)] configured and logged into the cluster in AD mode.

Follow the following procedure to reconfigure the cluster with encryption zones support.

1. After performing the upgrade with `azdata`, save the following scripts.

    Scripts execution requirements:
        
    * Both scripts should be located in the same directory. 
    * `kubectl` on `PATH
    * ```config``` file for Kubernetes in the folder ```$HOME/.kube```
    
    This script should be named __```run-key-provider-patch.sh```__:

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

    This script should be named __```updatekeyprovider.py```__:

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

    Execute __```run-key-provider-patch.sh```__ with the appropriate parameters. 

### Configuration of external providers

As mentioned in previous sections, a SQL Server 2019 CU8+ Big Data Cluster deployment will enable the encryption at rest functionality with system-managed keys by default.
In order to enable an external key provider to secure the root keys of encryption of SQL Server and HDFS see the following article: [External Key Providers in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](encryption-at-rest-external-provider.md)

## Next steps

To learn more about how key versions are used on [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]
 see: [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md)

To learn more about how to effectively use encryption at rest [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]
 see the following articles:

- [Encryption at rest - SQL Server TDE](encryption-at-rest-sql-server-tde.md)
- [Encryption at rest - HDFS encryption zones](encryption-at-rest-hdfs-encryption-zones.md)

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following overview:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
