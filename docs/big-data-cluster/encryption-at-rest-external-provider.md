---
title: External Key Providers
titleSuffix: SQL Server Big Data Clusters
description: This article provides details of how to configure external key providers for SQL Big Data Clusters encryption at rest.
ms.date: 06/11/2021
author: Danibunny
ms.author: dacoelho
ms.reviewer: wiassaf
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# External Key Providers in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

This article provides details of how to configure external key providers in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] for key management.

To learn more about how key versions are used on SQL Server Big Data Clusters, see: [Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-key-versions.md)

For information on configuring and using encryption at rest see the following guides:
* [Encryption at rest concepts and configuration guide](encryption-at-rest-concepts-and-configuration.md)
* [SQL Server Big Data Clusters HDFS Encryption Zones usage guide](encryption-at-rest-hdfs-encryption-zones.md)
* [SQL Server Big Data Clusters transparent data encryption (TDE) at rest usage guide](encryption-at-rest-sql-server-tde.md)

## <a id="prereqs"></a> Prerequisites

* [SQL Server 2019 Big Data Clusters release notes](release-notes-big-data-cluster.md). CU11+ required.
* [Big data tools](deploy-big-data-tools.md)
  * **azdata 20.3.5+**
* SQL Server Big Data Clusters user with administrative privileges.
* External provider template application. Download it from [here](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/security/encryption-at-rest-external-key-provider).

## Root key encryption using external providers

With the capability to bring in external keys in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], the main encryption key fetches the public key using the application that the customer deploys. When HDFS keys are rotated and used, the calls to decrypt the HDFS keys will be sent to the control plane, and then redirected to the application using the key identifier provided by the customer. For SQL Server, the requests to encrypt are sent and fulfilled by the control plane, since it has the public key. The requests to decrypt the Data Encryption Key (DEK) from SQL are sent to control plane as well, and then are redirected to the Key Management Server (KMS) application.

:::image type="content" source="media/big-data-cluster-key-versions/sql-customerkey.png" alt-text="After customer key is installed":::

The following diagram explains the interactions while configuring external keys in control plane:

:::image type="content" source="media/big-data-cluster-key-versions/external-key-control-pane-interactions.png" alt-text="Interactions while configuring external keys in control plane"  lightbox="media/big-data-cluster-key-versions/external-key-control-pane-interactions-LG.png":::  

After the key is installed, the encryption and decryption of different payloads are protected by main encryption key. This protection is similar to system-managed keys, except that the decryption calls routed to control plane, are then routed to the KMS plugin app. The KMS plugin app routes the request to appropriate location, such as the HSM or another product.

## Configuration

The provided template application is the plugin used to interface with the external key provider. This application needs to be customized and deployed into BDC to serve as an integration point with the chosen external key provider.

In the template application there are examples on how to integrate with HSM implementations using the standard PKCS11 protocol using [SoftHSM](https://www.opendnssec.org/softhsm). There is also an example using Hashicorp Vault. The template application is provided as-is as a reference implementation.

The following sections provide the steps required to configure an external key provider to serve as the root key of encryption for SQL Server databases and HDFS encryption zones.

### 1 - Create a RSA 2048 key in your external key provider

Create a PEM file with 2048 bit RSA key and upload it to the key value store in your external key provider.

e.g.: The key file may be added to the KV store in Hashicorp Vault at path bdc-encryption-secret and the name of the secret can be rsa2048.

### 2 - Customize and deploy the integration application on BDC

1. Unzip kms_plugin_app.zip that contains the BDC AppDeploy to communicate to external providers.
1. Customize the application. File ```custom.py``` contains the reference SoftHSM code and file ```custom2.py``` has a HashiCorp Vault example. Don't change the function contracts or signatures, as those are the integration points. Change only the function implementations if needed. ```app.py``` is the entry point that will load the application, understand it completely before deploying.
1. From the folder which has the spec.yaml deploy the application to BDC using: ```azdata app create -s```
1. . Wait for the application deployment to complete and the “Ready” status can be checked using ```azdata app list```

### 3 - Configure BDC to use the external key provider

Set the AZDATA_EXTERNAL_KEY_PIN environment variable to provide the token that allows access to the external key provider. `export AZDATA_EXTERNAL_KEY_PIN=<your PIN/token here>`

> [!NOTE]
> Note that the integration application deployment process uses the token to access your external key provider, however the AZDATA_EXTERNAL_KEY_PIN variable is saved encrypted in BDC control plane so that it can be interpreted by the application. Hence a different authentication mechanism can be used too, but the application will need to be changed. Check the ```custom*.py``` python application for the complete integration logic that’s being used.

Configure the key in BDC using the following azdata command structure, while changing the required parameters to your specific implementation. The following example uses a HashiCorp Vault structure as provided by ```custom2.py```.

```bash
azdata bdc kms update --app-name <YOUR-APP-NAME> --app-version <YOUR-APP-VERSION> \
--key-attributes keypath=<YOUR-KEY-PATH>,vaulturl=http://<YOUR-IP>:<YOUR-PORT>,keyname=<YOUR-KEY-NAME> \
--provider External
```

The ```--provider External``` command option will configure __BDC KMS__ to use the integration application as the endpoint for key operations.

Verify the root encryption key as the externally managed one using the following command.

```bash
azdata bdc kms show
```

### 4 - Encrypt your databases and encryption zones with the new keys

After the configuration, SQL Server databases and HDFS encryption zones are still encrypted by the previous key hierarchy. You need to explicitly encrypt using the externally managed keys.

In __SQL Server__ a new asymmetric key based on the externally managed key will be installed. Use that to encrypt your databases.

The asymmetric key can be seen using the following T-SQL query, with the `sys.asymmetric_keys` system catalog view.

```tsql
USE master;
select * from sys.asymmetric_keys;
```

The asymmetric key will appear with the naming convention "tde_asymmetric_key_<version>". The SQL Server administrator can then change the protector of the DEK to the asymmetric key using [ALTER DATABASE ENCRYPTION KEY](/sql/t-sql/statements/alter-database-encryption-key-transact-sql). For example, use the following T-SQL command:

```tsql
USE db1;
ALTER DATABASE ENCRYPTION KEY ENCRYPTION BY SERVER ASYMMETRIC KEY tde_asymmetric_key_0;
```

Use the following procedure to encrypt __each HDFS Encryption Zones__:

Execute the following command to check the current encryption key:

```bash
azdata bdc hdfs key describe
```

Get information about the version of the key protecting the EZ key:

```bash
azdata bdc hdfs key describe -n <key name>
```

Roll your key to the new external managed key:

```bash
azdata bdc hdfs key roll -n <new key name>
```

Start encryption:

```bash
azdata bdc hdfs encryption-zone reencrypt –path <your EZ path> --action start
```

Verify the key hierarchy using the following commands:

```bash
azdata bdc kms show
azdata bdc hdfs key describe
```


## See also

* [Security concepts for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](concept-security.md)  
* [Configure a SQL Server Big Data Cluster](configure-bdc-overview.md)
* [Big Data Clusters FAQ](big-data-cluster-faq.yml)
