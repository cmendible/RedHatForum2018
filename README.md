[![Build Status](https://mendible.visualstudio.com/mendible/_apis/build/status/cmendible.RedHatForum2018)](https://mendible.visualstudio.com/mendible/_build/latest?definitionId=5)

# RedHatForum2018
Red Hat Forum Spain 2018

## Deploy Openshift on Azure

Use ARM tempalte from: [Microsoft/openshift-origin](https://github.com/Microsoft/openshift-origin)

And run the following commands:

``` shell
az ad sp create-for-rbac -n openshiftcloudprovider --password [changeme] --role contributor
az group create -n RedHat2018 -l westeurope
az keyvault create -n RedHatSpain2018 -g RedHat2018 -l westeurope --enabled-for-template-deployment true
az keyvault secret set --vault-name RedHatSpain2018 -n secret --file ..\id_rsa
az group create --name OpenShift2018 --location westeurope
az group deployment create --resource-group OpenShift2018 --template-file azuredeploy.json --parameters azuredeploy.parameters.json --no-wait
```

## Deploy Openshift on AWS

Follow instructions shown here: [dwmkerr/terraform-aws-openshift](https://github.com/dwmkerr/terraform-aws-openshift)

## Deploy Open Service Broker for Azure:

Run the following commands for each Openshift Cluster:
``` shell
oc new-project osba
oc process -f https://raw.githubusercontent.com/Azure/open-service-broker-azure/master/contrib/openshift/osba-os-template.yaml -p ENVIRONMENT=AzurePublicCloud  -p AZURE_SUBSCRIPTION_ID=[changeme] -p AZURE_TENANT_ID=[changeme] -p AZURE_CLIENT_ID=http://openshiftcloudprovider -p AZURE_CLIENT_SECRET=[changeme] | oc create -f -
```

## Deploy the Couchbase Autonomous Operator 1.0 for Red Hat OpenShift:

Download the Couchbase Autonomous Operator 1.0 for Red Hat OpenShift from: [https://www.couchbase.com/downloads/](https://www.couchbase.com/downloads/)

Extract and run run the following commands for each Openshift Cluster:
``` shell
oc create -f crd.yaml
oc create secret docker-registry rh-catalog --docker-server=registry.connect.redhat.com --docker-username=[changeme] --docker-password=[changeme] --docker-email=[changeme]
oc create -f cluster-role-sa.yaml
oc create serviceaccount couchbase-operator --namespace default
oc secrets add serviceaccount/couchbase-operator secrets/rh-catalog --for=pull
oc secrets add serviceaccount/default secrets/rh-catalog --for=pull
oc create clusterrolebinding couchbase-operator-rolebinding --clusterrole couchbase-operator --serviceaccount default:couchbase-operator
```

## Deploy the sample cluster
Run the following commands for each Openshift Cluster:

``` shell
oc create -f operator.yaml -n default
oc create -f secret.yaml
bin\cbopctl.exe create -f couchbase-cluster.yaml
```

## Deploy Azure Database for PostgreSQL

From de K8s folder of this repo, run the following commands for each Openshift Cluster:
``` shell
oc apply -f .\postgresql-instance.yaml
oc apply -f .\postgresql-binding.yaml
```

## Deploy the application:

Build the image from the src directory of this:
``` shell
docker build -t [openshift registry name]/default/redhatforum:0.1.0 .
docker push [openshift registry name]/default/redhatforum:0.1.0
```

For each Openshift Cluster, deploy the application from the k8s folder of this repo running:
```
oc apply -f temperature.yaml
```
