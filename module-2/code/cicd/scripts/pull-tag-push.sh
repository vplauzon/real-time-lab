#!/bin/bash

##########################################################################
##  Pull, tag and push a container
##
##  Inputs:
##      image:      Image name
##      fullTag:    Tag with minor version
##      targetTag:  Target tag

image=$1
fullTag=$2
targetTag=$3

echo
echo "Image name:  $image"
echo "Full tag:  $fullTag"
echo "Target tag:  $targetTag"

echo
echo "Pull"

docker pull "$image:$fullTag"

echo
echo "Tag"

docker tag "$image:$fullTag" "$image:$targetTag"

echo
echo "Push"

docker push "$image:$targetTag"