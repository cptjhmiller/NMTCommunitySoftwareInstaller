#!/bin/bash

echo " --------------------------------------------------------- "
echo "   STARTING NMT COMMUNITY SOFTWARE INSTALLER"
echo " --------------------------------------------------------- "
echo "   DEMARRAGE DE L'INSTALLATION DE NMT COMMUNITY SOFTWARE"
echo " --------------------------------------------------------- "
echo
echo

#Check if mono is there
mono --version >/dev/null 2>/dev/null
if [ "$?" == "0" ]; then
    # rm -Rf ~/.local/share/Ger\ Teunis/ >/dev/null 2>&1
    mono "NMT Community Software Installer.exe"
else
    echo "ERROR: PLEASE READ!"
    echo
    echo "Can't find Mono, without Mono this application can't run"
    echo "Please download Mono from http://www.mono-project.com"
    echo "Do not use version 2.2!"
    echo
    echo "You also need to install libmono-winforms."
    echo
    echo
    echo "ERREUR: SVP LISEZ!"
    echo
    echo "Mono est introuvable, cette aplication ne peut fonctionner sans Mono"
    echo "Veuillez télécharger Mono à partir de http://www.mono-project.com"
    echo "N'utilisez-pas la version 2.2!"
    echo
    echo "Vous aurez aussi besoin d'installer libmono-winforms."
    sleep 600
fi