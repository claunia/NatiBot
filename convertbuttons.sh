#!/bin/bash
for i in *.bmp; do j=`basename "$i" .bmp`; convert -quality 90 -transparent 'rgb(237,28,36)' "$i" "${j}.png"; done
