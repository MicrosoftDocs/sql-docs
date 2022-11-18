---
title: Deploy on Azure Red Hat OpenShift python script
titleSuffix: SQL Server Big Data Clusters
description: Learn how to use a deployment script to deploy SQL Server Big Data Clusters on Azure Red Hat OpenShift (ARO).
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Use a python script to deploy a SQL Server Big Data Cluster on Azure Red Hat OpenShift (ARO)

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

In this tutorial, you use a sample python deployment script to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] to [Azure Red Hat OpenShift (ARO)](/azure/virtual-machines/linux/openshift-get-started). This deployment option is supported beginning with SQL Server 2019 CU5.

> [!TIP]
> ARO is only one option for hosting Kubernetes for your big data cluster. To learn about other deployment options as well as how to customize deployment options, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md).


> [!WARNING]
> Persistent volumes created with the built-in storage class *managed-premium* have a reclaim policy of *Delete*. So, when you delete the SQL Server big data cluster, persistent volume claims are deleted as are the persistent volumes. You should create custom storage classes by using azure-disk provisioner with a *Retain* reclaim policy, as described in [Concepts storage](/azure/aks/concepts-storage/#storage-classes). The script below is using the *managed-premium* storage class. See [Data persistence](concept-data-persistence.md) topic for more details.

The default big data cluster deployment used here consists of a SQL Master instance, one compute pool instance, two data pool instances, and two storage pool instances. Data is persisted using Kubernetes persistent volumes that use the ARO default storage classes. The default configuration used in this tutorial is suitable for dev/test environments.

## Prerequisites

- An Azure subscription.
- [OpenShift CLI (oc)](https://docs.openshift.com/container-platform/4.4/cli_reference/openshift_cli/getting-started-cli.html)
- [Python minimum version 3.0](https://www.python.org/downloads)
- [`az` CLI](/cli/azure/install-azure-cli/)
- [[!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]](../azdata/install/deploy-install-azdata.md)
- [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md)

## Log in to your Azure account

The script uses Azure CLI to automate the creation of an ARO cluster. Before running the script, you must log in to your Azure account with Azure CLI at least once. Run the following command from a command prompt.

```azurecli
az login
```

## Instructions

1. Download both the Python script `deploy-sql-big-data-aro.py` and the yaml file `bdc-scc.yaml`.

   >These files are located in this article under:
   - [`deploy-sql-big-data-aro.py`](#deploy-sql-big-data-aropy)
   - [`bdc-scc.yaml`](#bdc-sccyaml)

1. Run the script using:

```console
python deploy-sql-big-data-aro.py
```

When prompted, provide your input for Azure subscription ID and the Azure resource group to create the resources in. Optionally, you can also provide your input for other configurations or use the defaults provided. For example:

- `azure_region`
- `vm_size` for OpenShift worker nodes. For an optimal experience while you are validating basic scenarios, we recommend at least 8 vCPUs and 64-GB memory across all worker nodes in the cluster. The script uses `Standard_D8s_v3` and three worker nodes as default. A default size configuration for big data clusters also uses about 24 disks for persistent volume claims across all components.
- network configuration for OpenShift cluster deployment - see the [ARO deployment article](\azure\openshift\tutorial-create-cluster) for more details on each parameter.
- `cluster_name` - this value is used for both ARO cluster and SQL Server Big Data Cluster created on top of ARO. Note that the name of the SQL Big Data Cluster is going to be a Kubernetes namespace.
- `username `- this is the username for the accounts provisioned during deployment for the controller admin account, SQL Server master instance account, and gateway. Note that `sa` SQL Server account is disabled automatically for you, as a best practice.
- `password` - same value is going to be used for all accounts.

The SQL Server Big Data Cluster is now deployed on ARO. You can now use Azure Data Studio to connect to the cluster. For more information, see [Connect to a SQL Server big data cluster with Azure Data Studio](connect-to-big-data-cluster.md).

## Clean up

If you are testing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Azure, you should delete the ARO cluster when finished to avoid unexpected charges. Do not remove the cluster if you intend to continue using it.

> [!WARNING]
> The following steps tears down the ARO cluster which removes the SQL Server big data cluster as well. If you have any databases or HDFS data that you want to keep, back that data up before deleting the cluster.

Run the following Azure CLI command to remove the big data cluster and the ARO service in Azure (replace `<resource group name>` with the **Azure resource group** you specified in the deployment script):

```azurecli
az group delete -n <resource group name>
```

## `deploy-sql-big-data-aro.py` 

The script in this section deploys the SQL Server Big Data Cluster to Azure Red Hat OpenShift. Copy the script to your workstation and save it as `deploy-sql-big-data-aro.py` before you begin the deployment.

```python
#
# Prerequisites: 
# 
# Azure CLI, Azure Data CLI (`azdata`), OpenShift CLI (oc)  
#
# Run `az login` at least once BEFORE running this script
#

from subprocess import check_output, CalledProcessError, STDOUT, Popen, PIPE, getoutput
from time import sleep
import os
import getpass
import json

def executeCmd (cmd):
    if os.name=="nt":
        process = Popen(cmd.split(),stdin=PIPE, shell=True)
    else:
        process = Popen(cmd.split(),stdin=PIPE)
    stdout, stderr = process.communicate()
    if (stderr is not None):
        raise Exception(stderr)

#
# MUST INPUT THESE VALUES!!!!!
#
SUBSCRIPTION_ID = input("Provide your Azure subscription ID:").strip()
GROUP_NAME = input("Provide Azure resource group name to be created:").strip()
#
# This password will be use for Controller user, Gateway user and SQL Server Master SA accounts
AZDATA_USERNAME=input("Provide username to be used for Controller, SQL Server and Gateway endpoints - Press ENTER for using  `admin`:").strip() or "admin"
AZDATA_PASSWORD = getpass.getpass("Provide password to be used for Controller user, Gateway user and SQL Server Master accounts:")
#
# Optionally change these configuration settings
#
AZURE_REGION=input("Provide Azure region - Press ENTER for using `westus2`:").strip() or "westus2"
# MASTER_VM_SIZE=input("Provide VM size for master nodes for the ARO cluster - Press ENTER for using  `Standard_D2s_v3`:").strip() or "Standard_D2s_v3"
WORKER_VM_SIZE=input("Provide VM size for the worker nodes for the ARO cluster - Press ENTER for using  `Standard_D8s_v3`:").strip() or "Standard_D8s_v3"
OC_NODE_COUNT=input("Provide number of worker nodes for ARO cluster - Press ENTER for using  `3`:").strip() or "3"
VNET_NAME=input("Provide name of Virtual Network for ARO cluster - Press ENTER for using  `aro-vnet`:").strip() or "aro-vnet"
VNET_ADDRESS_SPACE=input("Provide Virtual Network Address Space for ARO cluster - Press ENTER for using  `10.0.0.0/16`:").strip() or "10.0.0.0/16"
MASTER_SUBNET_NAME=input("Provide Master Subnet Name for ARO cluster - Press ENTER for using  `master-subnet`:").strip() or "master-subnet"
MASTER_SUBNET_IP_RANGE=input("Provide address range of Master Subnet for ARO cluster - Press ENTER for using  `10.0.0.0/23`:").strip() or "10.0.0.0/23"
WORKER_SUBNET_NAME=input("Provide Worker Subnet Name for ARO cluster - Press ENTER for using  `worker-subnet`:").strip() or "worker-subnet"
WORKER_SUBNET_IP_RANGE=input("Provide address range of Worker Subnet for ARO cluster - Press ENTER for using  `10.0.2.0/23`:").strip() or "10.0.2.0/23"
#
# This is both Kubernetes cluster name and SQL Big Data cluster name
CLUSTER_NAME=input("Provide name of OpenShift cluster and SQL big data cluster - Press ENTER for using  `sqlbigdata`:").strip() or "sqlbigdata"
#
# Deploy the ARO cluster
#  
print ("Set azure context to subscription: "+SUBSCRIPTION_ID)
command = "az account set -s "+ SUBSCRIPTION_ID
executeCmd (command)
print ("Creating azure resource group: "+GROUP_NAME)
command="az group create --name "+GROUP_NAME+" --location "+AZURE_REGION
executeCmd (command)
command = "az network vnet create --resource-group "+GROUP_NAME+" --name "+VNET_NAME+" --address-prefixes "+VNET_ADDRESS_SPACE
print("Creating Virtual Network: "+VNET_NAME)
executeCmd(command)
command = "az network vnet subnet create --resource-group "+GROUP_NAME+" --vnet-name "+VNET_NAME+" --name "+MASTER_SUBNET_NAME+" --address-prefixes "+MASTER_SUBNET_IP_RANGE+" --service-endpoints Microsoft.ContainerRegistry"
print("Creating Master Subnet: "+MASTER_SUBNET_NAME)
executeCmd(command)
command = "az network vnet subnet create --resource-group "+GROUP_NAME+" --vnet-name "+VNET_NAME+" --name "+WORKER_SUBNET_NAME+" --address-prefixes "+WORKER_SUBNET_IP_RANGE+" --service-endpoints Microsoft.ContainerRegistry"
print("Creating Worker Subnet: "+WORKER_SUBNET_NAME)
executeCmd(command)
command = "az network vnet subnet update --name "+MASTER_SUBNET_NAME+" --resource-group "+GROUP_NAME+" --vnet-name "+VNET_NAME+" --disable-private-link-service-network-policies true"
print("Updating Master Subnet by disabling Private Link Policies")
executeCmd(command)
command = "az aro create --resource-group "+GROUP_NAME+" --name "+CLUSTER_NAME+" --vnet "+VNET_NAME+" --master-subnet "+MASTER_SUBNET_NAME+" --worker-subnet "+WORKER_SUBNET_NAME+" --worker-count "+OC_NODE_COUNT+" --worker-vm-size "+WORKER_VM_SIZE +" --only-show-errors"
print("Creating OpenShift cluster: "+CLUSTER_NAME)
executeCmd (command)
#
# Login to oc console
#
command = "az aro list-credentials --name "+CLUSTER_NAME+" --resource-group "+GROUP_NAME +" --only-show-errors"
output=json.loads(getoutput(command))
OC_CLUSTER_USERNAME = str(output['kubeadminUsername'])
OC_CLUSTER_PASSWORD = str(output['kubeadminPassword'])
command = "az aro show --name "+CLUSTER_NAME+" --resource-group "+GROUP_NAME +" --only-show-errors"
output=json.loads(getoutput(command))
APISERVER = str(output['apiserverProfile']['url'])
command = "oc login "+ APISERVER+ " -u " + OC_CLUSTER_USERNAME + " -p "+ OC_CLUSTER_PASSWORD
executeCmd (command)
#
# Setup pre-requisites for deploying BDC on OpenShift
#
#
#Creating new project/namespace
command = "oc new-project "+ CLUSTER_NAME
executeCmd (command)
#
# create custom SCC for BDC
command = "oc apply -f bdc-scc.yaml"
executeCmd (command)
#
#Bind the custom scc with service accounts in the BDC namespace
command = "oc create clusterrole bdc-role --verb=use --resource=scc --resource-name=bdc-scc -n " + CLUSTER_NAME
executeCmd (command)
command = "oc create rolebinding bdc-rbac --clusterrole=bdc-role --group=system:serviceaccounts:" + CLUSTER_NAME
executeCmd (command)
#
# Deploy big data cluster
#
print ('Set environment variables for credentials')
os.environ['AZDATA_PASSWORD'] = AZDATA_PASSWORD
os.environ['AZDATA_USERNAME'] = AZDATA_USERNAME
os.environ['ACCEPT_EULA']="Yes"
#
#Creating azdata configuration with aro-dev-test profile
command = "azdata bdc config init --source aro-dev-test --target custom --force"
executeCmd (command)
command="azdata bdc config replace -c custom/bdc.json -j ""metadata.name=" + CLUSTER_NAME + ""
executeCmd (command)
#
# Create BDC
command = "azdata bdc create --config-profile custom --accept-eula yes"
executeCmd(command)
#login into big data cluster and list endpoints
command="azdata login -n " + CLUSTER_NAME
executeCmd (command)
print("")
print("SQL Server big data cluster endpoints: ")
command="azdata bdc endpoint list -o table"
executeCmd(command)
```

## `bdc-scc.yaml`

The following .yaml manifest defines a custom security context constraint (SCC) for the Big Data Cluster deployment. Copy it to the same directory as `deploy-sql-big-data-aro.py`.

```yaml
allowHostDirVolumePlugin: false
allowHostIPC: false
allowHostNetwork: false
allowHostPID: false
allowHostPorts: false
allowPrivilegeEscalation: true
allowPrivilegedContainer: false
allowedCapabilities:
  - SETUID
  - SETGID
  - CHOWN
  - SYS_PTRACE
apiVersion: security.openshift.io/v1
defaultAddCapabilities: null
fsGroup:
  type: RunAsAny
kind: SecurityContextConstraints
metadata:
  annotations:
    kubernetes.io/description: SQL Server BDC custom scc is based on 'nonroot' scc plus additional capabilities required by BDC.
  generation: 2
  name: bdc-scc
readOnlyRootFilesystem: false
requiredDropCapabilities:
  - KILL
  - MKNOD
runAsUser:
  type: MustRunAsNonRoot
seLinuxContext:
  type: MustRunAs
supplementalGroups:
  type: RunAsAny
volumes:
  - configMap
  - downwardAPI
  - emptyDir
  - persistentVolumeClaim
  - projected
  - secret
```

## Next steps

The deployment script configured Azure Kubernetes Service and also deployed a SQL Server 2019 big data cluster. You can also choose to customize future deployments through manual installations. To learn more about how big data clusters are deployed as well as how to customize deployments, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md).

Now that the SQL Server big data cluster is deployed, you can load sample data and explore the tutorials:

> [!div class="nextstepaction"]
> [Tutorial: Load sample data into a SQL Server 2019 big data cluster](tutorial-load-sample-data.md)
