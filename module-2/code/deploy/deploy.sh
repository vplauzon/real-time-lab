#!/bin/bash

##########################################################################
##  Deploys simulator solution
##
##  Takes 1 parameter:
##
##  1- Name of resource group

rg=$1

echo "Resource group:  $rg"
echo "Deployment Type:  $type"

echo
echo "Deploying ARM template"

az deployment group create -n "deploy-$(uuidgen)" -g $rg \
    --template-file deploy.json 
    