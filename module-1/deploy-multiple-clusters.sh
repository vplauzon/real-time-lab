#!/bin/bash

##########################################################################
##  Deploys multiple dev clusters
##
##  Takes 1 parameter:
##
##  1- Name of resource group
##  2- Number of clusters

rg=$1
clusterCount=$2

echo "Resource group:  $rg"
echo "Cluster count:  $clusterCount"

echo
echo "Deploying ARM template"

az deployment group create -n "deploy-$(uuidgen)" -g $rg \
    --template-file deploy-multiple-clusters.json \
    --parameters clusterCount=$clusterCount

