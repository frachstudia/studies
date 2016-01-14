@echo off

pdflatex thesis-bachelor-polski.tex 
bibtex   thesis-bachelor-polski
pdflatex thesis-bachelor-polski.tex 
pdflatex thesis-bachelor-polski.tex 

del *.aux *.bak *.log *.blg *.bbl *.toc *.out
