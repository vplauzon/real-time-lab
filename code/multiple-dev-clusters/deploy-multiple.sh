#!/bin/bash

##########################################################################
##  Deploys multiple dev clusters
##
##  Takes 3 parameters:
##
##  1- Name of resource group
##  2- Cluster prefix
##  3- Cluster count (number of clusters)

rg=$1
clusterPrefix=$2
clusterCount=$3

echo "Resource group:  $rg"
echo "Cluster prefix:  $clusterPrefix"
echo "Number of clusters:  $clusterCount"

echo
echo "Deploying ARM template"

az deployment group create -n "deploy-$(uuidgen)" -g $rg \
    --template-file deploy-multiple.json 
    --parameters clusterPrefix=$clusterPrefix clusterCount=$clusterCount