---
title: Update AZDATA_PASSWORD
description: Update the `AZDATA_PASSWORD` manually
author: NelGson
ms.author: negust
ms.reviewer: mikeray
ms.date: 12/19/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manually update `AZDATA_PASSWORD`

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

Whether or not the cluster is operating with Active Directory integration, `AZDATA_PASSWORD` is set during deployment. It provides a basic authentication to the cluster controller and master instance. This document describes how to manually update `AZDATA_PASSWORD`.

## Change `AZDATA_PASSWORD` for controller

If the cluster is operating in non-Active Directory mode, update the Apache Knox Gateway password by doing the following:

1. Obtain the controller SQL Server credentials by running the following commands:

   a. Run this command as a Kubernetes administrator:

   ```bash
   kubectl get secret controller-sa-secret -n <cluster name> -o yaml | grep password
   ```

   b. Base64 decode the secret:
   
   ```bash
   echo <password from kubectl command>  | base64 --decode && echo
   ```

1. In a separate command window, expose the controller database server port:

   ```bash
   kubectl port-forward controldb-0 1433:1433 --address 0.0.0.0 -n <cluster name>
   ```
 
1. Use the system administrator password, which you just obtained, to connect to the controller database server from a SQL client tool.

1. Generate a new complex password for `AZDATA_USERNAME` to replace the existing `AZDATA_PASSWORD`.

   To simplify the example, the next steps use "newPassword" because the generated password is "newPassword". 

1. Get `hexsalt` from the users table:

   ```sql
   SELECT hexsalt FROM [auth].[users] WHERE username = '<username>'
   ```

   `hexsalt` returns a random hex string (for example, `64FC59DF31244FFEE02F457BC0750226`).

1. Encrypt the new complex password by using `hexsalt`:

   For your convenience, we provide a pre-built tool `pbkdf2` to encrypt the password. Download the platform-appropriate .NET Core app for [`pbkdf2`](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/security/password-hashing/pbkdf2/prebuilt-binaries).

   The app is self-contained and requires no prerequisites, such as .NET runtimes. To encrypt the password run:

   ```bash
   pbkdf2 <password> <hexsalt>
   J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=
   ```

1. Update the password in the users table:

   ```SQL
   UPDATE [auth].[users] SET password = 'J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=' WHERE username = '<username>'
   ```

## Change `AZDATA_PASSWORD` in the SQL Server master instance

1. Connect to the master SQL endpoint with any administrator user.

1. To change the password for the login credentials that you defined during deployment in the parameter `AZDATA_USERNAME`, run the following TSQL command:

   ```sql
   ALTER LOGIN <AZDATA_USERNAME> WITH PASSWORD = 'newPassword'
   ```
### Manually updating password for Grafana and Kibana
After following the steps to update AZDATA_PASSWORD we would see that Grafana and Kibana are still accepting the old password and not the new password. This is because Grafana and Kibana does not have the ability to pick the new k8s secret. So we need to manually update the password for both of them separately.

### Steps to update Grafana Password:
 

- We will need htpasswd utility. You can install this on any client machine.
    - [ ] For Ubuntu: 
		`sudo apt install apache2-utils`
    - [ ] For RHEL:
		`sudo yum install httpd-tools`
	
- Generate the new password , where 'admin' is the user and 'Test@12345' is its new password:

                htpasswd -nbs admin Test@12345
                admin:{SHA}W/5VKRjIzjusUJ0ih0gHyEPjC/s=

- Now encode the password:

		echo "admin:{SHA}W/5VKRjIzjusUJ0ih0gHyEPjC/s=" | base64
		IGFkbWluOntTSEF9Vy81VktSakl6anVzVUowaWgwZ0h5RVBqQy9zPQo= 
			 

- Now we need to edit the mgmtproxy-secret:

		kubectl edit secret -n mssql-cluster mgmtproxy-secret
		 

- Update the controller-login-htpasswd with the new value generated above:

		# Please edit the object below. Lines beginning with a '#' will be ignored,
		# and an empty file will abort the edit. If an error occurs while saving this file will be
		# reopened with the relevant failures.
		#
		apiVersion: v1
		data:
		  controller-login-htpasswd: IGFkbWluOntTSEF9Vy81VktSakl6anVzVUowaWgwZ0h5RVBqQy9zPQo=
		  mssql-internal-controller-password: xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		  mssql-internal-controller-username: xxxxxxxxxxxxxxxxxx
		 

- Delete the mgmtproxy pod:

		kubectl delete pod mgmtproxy-xxxxx -n mssql-clutser
			 

- Wait for the mgmtproxy pod to come online and Grafana Dashboard to start. 

- Now login to Grafana using new password.

### Steps to update Kibana Password:

- Open kibana URL:

			https://11.111.111.111:30777/kibana/app/kibana#/discover
		 

- Go to -> Security -> Internal User Database -> Edit -> Reset Password:

    ![image](https://user-images.githubusercontent.com/54091686/108350492-4783ee00-720a-11eb-927d-d08c2b73fa60.png)
    
    ![image](https://user-images.githubusercontent.com/54091686/108350588-684c4380-720a-11eb-9d22-e3c73f00cc07.png)
    
    ![image](https://user-images.githubusercontent.com/54091686/108350595-6b473400-720a-11eb-9477-6601106807c2.png)

- Now reconnect to Kibana using the new password.
