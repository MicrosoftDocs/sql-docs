---
title: "Quickstart: Deploy SQL Server on Linux using an Ansible playbook"
description: Learn how to deploy SQL Server on Linux to several managed nodes using Ansible playbook.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh
ms.date: 08/17/2022
ms.service: sql
ms.subservice: linux
ms.topic: quickstart
---

# Quickstart: Deploy SQL Server on Linux using an Ansible playbook

[!INCLUDE [sql-linux](../includes/applies-to-version/sql-linux.md)]

This quickstart takes you through the steps to automate a SQL Server on Linux deployment on Azure Virtual Machines, using an [Ansible](/azure/developer/ansible/overview) playbook.

[Ansible](https://www.ansible.com/) is an open-source product that automates cloud provisioning, configuration management, and application deployments.

[Ansible playbooks](https://docs.ansible.com/ansible/latest/playbooks.html) allow you to direct Ansible to configure your environment. Playbooks are coded using YAML so as to be human-readable.

## Prerequisites

- An Azure subscription. If you don't have an Azure subscription, you can create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F).

- Create a new [resource group](/cli/azure/manage-azure-groups-azure-cli#create-a-resource-group) using Azure CLI, which contains three Azure Virtual Machines (VMs):

  - [Create an Azure VM](/azure/virtual-machines/linux/create-cli-complete), running Red Hat Enterprise Linux (RHEL) 8.5 or higher. This VM will become the *controller node*.

  - [Create an Azure VM](/azure/virtual-machines/linux/create-cli-complete), running RHEL, to serve as the first *managed node*.

  - [Create an Azure VM](/azure/virtual-machines/linux/create-cli-complete), running Ubuntu Server, to serve as the second *managed node*.

## Overview

The first VM, where you configure Ansible Core, is the controller node. On this node, you'll install the SQL Server *system role*.

The remaining VMs will be the target machines, also known as *managed nodes*, for deploying and configuring SQL Server using the system role.

## Install Ansible Core

Starting with RHEL 8.5 on Azure VMs, the `ansible-core` package can be installed from the pre-configured AppStream repository. You can install Ansible Core on the controller node using the following command:

```bash
sudo yum install ansible-core
```

You can check that the installation was successful with the following command:

```bash
ansible --version
```

You will see output similar to the following example:

```output
ansible [core 2.12.2]
  config file = /etc/ansible/ansible.cfg
  configured module search path = ['/home/<user>/.ansible/plugins/modules', '/usr/share/ansible/plugins/modules']
  ansible python module location = /usr/lib/python3.8/site-packages/ansible
  ansible collection location = /home/<user>/.ansible/collections:/usr/share/ansible/collections
  executable location = /usr/bin/ansible
  python version = 3.8.12 (default, Sep 16 2021, 10:46:05) [GCC 8.5.0 20210514 (Red Hat 8.5.0-3)]
  jinja version = 2.10.3
  libyaml = true
```

## Edit `hosts` file on controller node

Ansible will create a `hosts` file is in the `/etc/ansible` directory. Edit this file using your favorite editor to add the managed node details, either as a group entry, or as ungrouped entries. For information on how to create your own inventory, see [How to build your inventory](https://docs.ansible.com/ansible/latest/user_guide/intro_inventory.html).

In this example using the `hosts` file, the IP address for the first managed node is 10.0.0.12, and the IP address for the second managed node is 10.0.0.14.

```output
# This is the default ansible 'hosts' file.
#
# It should live in /etc/ansible/hosts
#
#   - Comments begin with the '#' character
#   - Blank lines are ignored
#   - Groups of hosts are delimited by [header] elements
#   - You can enter hostnames or ip addresses
#   - A hostname/ip can be a member of multiple groups

10.0.0.12
10.0.0.14
```

## Configure passwordless SSH access between nodes

You'll need to configure a Secure Shell (SSH) connection between the controller node and all managed nodes where SQL Server is to be installed.

### Configure SSH on the controller node

If SSH has already been configured, you can skip this step.

Use the `ssh-keygen` command to generate SSH keys. When you run the command, you'll be prompted to accept the default values. When complete, you'll have a private and public key pair.

### Copy the public key to the managed nodes

1. On each managed node, you must copy the public key from the controller node you just created, using the `ssh-copy-id` command. If you want to specify the target directory on the managed node, you can use the `-i` parameter.

1. In the following command, the `user` account can be the same account you configured for each managed node when creating the VM. You can also use the `root` account, but this isn't recommended in a production environment.

    ```bash
    sudo ssh-copy-id user@10.0.0.12
    sudo ssh-copy-id user@10.0.0.14
    ```

1. To confirm that the SSH public key was copied to each node, use the `ssh` command from the controller node. If you copied the keys correctly, you won't be prompted for a password, and the connection will be successful.

    ```bash
    ssh user@10.0.0.12
    ssh user@10.0.0.14
    ```

## Install the SQL Server system role

The Ansible system role is called `ansible-collection-microsoft-sql`. On the controller node, run the following command to install the SQL Server system role:

```bash
sudo yum install ansible-collection-microsoft-sql
```

This command installs the SQL Server role to `/usr/share/ansible/collections`, with the files shown below:

```output
-rw-r--r--. 1 user user 7592 Jul  2 20:22 FILES.json
-rw-r--r--. 1 user user 1053 Jul  2 20:22 LICENSE-server
-rw-r--r--. 1 user user  854 Jul  2 20:22 MANIFEST.json
-rw-r--r--. 1 user user 1278 Jul  2 20:22 README.md
drwxr-xr-x. 1 user user   20 Jul  2 20:22 roles
drwxr-xr-x. 1 user user   20 Jul  2 20:22 tests
```

## Create and configure the Ansible playbook

After installing the system role, you'll create the SQL Server playbook YAML file. To understand the various role variables, refer to the [documentation](https://github.com/linux-system-roles/mssql/blob/master/README.md) or the README.md included with the SQL Server system role.

The following example shows a playbook file, with role variables defined to configure SQL Server and enable additional functionality:

```yaml
- hosts: all
  vars:
    mssql_accept_microsoft_odbc_driver_17_for_sql_server_eula: true
    mssql_accept_microsoft_cli_utilities_for_sql_server_eula: true
    mssql_accept_microsoft_sql_server_standard_eula: true
    mssql_password: "YourP@ssw0rd"
    mssql_edition: Evaluation
    mssql_enable_sql_agent: true
    mssql_install_fts: true
    mssql_install_powershell: true
    mssql_tune_for_fua_storage: true
  roles:
    - microsoft.sql.server​
```

## Deploy SQL Server on the managed nodes

To deploy SQL Server on managed nodes using the Ansible playbook, run the following command from the controller node.

```bash
sudo ansible-playbook -u user playbook.yaml
```

This process will begin the deployment, and at the end, you should see a summary of the play that looks similar to this:

```output
PLAY RECAP *******

10.0.0.12                  : ok=31   changed=42   unreachable=0    failed=0    skipped=0   rescued=1    ignored=0

10.0.0.14                  : ok=31   changed=42   unreachable=0    failed=0    skipped=0   rescued=1    ignored=0
```

## Clean up resources

If you're not going to continue using your Azure VMs, remember to remove them. If you created the three VMs in a new resource group, you can remove all the resources inside that resource group using [Azure CLI](/cli/azure/manage-azure-groups-azure-cli#clean-up-resources).

## See also

- [Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts](sql-server-linux-containers-deploy-helm-charts-kubernetes.md)

## Next steps

- [Introduction to adutil - Active Directory utility](sql-server-linux-ad-auth-adutil-introduction.md)
- [Backup and restore SQL Server databases on Linux](sql-server-linux-backup-and-restore-database.md)
- [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md)
