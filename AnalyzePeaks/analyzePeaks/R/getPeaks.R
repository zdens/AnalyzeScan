#' Extract meaningfull peaks from spectra
#'
#' @param spectra list of \code{MALDIquant} \code{\link{AbstractMassObject}})
#' @param model \code{\link{glmnet}} model to deal with
#' @param calibMethod method of spectra normalization
#' @param noiseMethod method of SNR estimation
#' @param tolerance tolerance for binning of peaks
#' @param minFrequency minimal frequency of
#' @param SNR
#' @param halfWindowSize
#'
#' @return matrix of peaks intensities suitable for \code{\link{predict}}
#' @export
#'
getPeaks<-function(spectra,model,
                   calibMethod="TIC",
                   noiseMethod="SuperSmoother",
                   tolerance = 2e-4,
                   minFrequency=0.25,
                   SNR=2,
                   halfWindowSize=5
){
  s1 <- calibrateIntensity(s, method=calibMethod)
  peaks1 <- detectPeaks(s1, SNR=SNR,
                        halfWindowSize=halfWindowSize,
                        method=noiseMethod)
  ref<-getReference(model)
  wf<-determineWarpingFunctions(peaks1,
                                method="lowess",
                                plot=FALSE,
                                reference = ref)
  aPeaks<-warpMassPeaks(peaks1,wf)
  bPeaks <- binPeaks(aPeaks, tolerance=tolerance)
  fpeaks <- filterPeaks(bPeaks,
                        minFrequency=minFrequency)
  featureMatrix <- intensityMatrix(fpeaks)
  idNA<-which(is.na(featureMatrix),arr.ind =TRUE)
  featureMatrix[idNA]<-0
  return(featureMatrix)
}

#' Calculate Predictions
#'
#' @param peaks matrix from \code{\link{getPeaks}}
#' @param model model \code{\link{glmnet}} model to deal with
#'
#' @return list with values: class, probability of class I, probability of class II
#' @export
#'
getPrediction<-function(peaks,model){
  prClass<-predict(model,peaks,type='class',s="lambda.min")
  prProb<-predict(model,peaks,type='response',s="lambda.min")
  #prCfs<-predict(model,peaks,type='coefficients',s="lambda.min")
 return(list(class=prClass,probI=(1-prProb),probII=prProb)) # add coefficients later
}

#' Prepare reference MassPeaks for warping of the input
#'
#' @param model  \code{\link{glmnet}} model to deal with
#'
#' @return \code{\link{MassPeaks}} object
#' @export
#'
#' @examples
getReference<-function(model){
  mz<-as.numeric(dimnames(model$glmnet.fit$beta)[[1]])
  refPeaks<-createMassPeaks(mz,rep(1e6,length(mz)))
  return(refPeaks)
}
