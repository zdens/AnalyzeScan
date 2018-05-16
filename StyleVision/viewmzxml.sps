<?xml version="1.0" encoding="UTF-8"?>
<structure version="20" xsltversion="1" html-doctype="HTML4 Transitional" compatibility-view="IE9" html-outputextent="Complete" relativeto="*SPS" encodinghtml="UTF-8" encodingrtf="ISO-8859-1" encodingpdf="UTF-8" useimportschema="1" embed-images="1" enable-authentic-scripts="1" authentic-scripts-in-debug-mode-external="0" generated-file-location="DEFAULT">
	<parameters/>
	<schemasources>
		<namespaces>
			<nspair prefix="cff" uri="http://sashimi.sourceforge.net/schema_revision/mzXML_2.1"/>
		</namespaces>
		<schemasources>
			<xsdschemasource name="XML2" schemafile="C:\Users\den\source\repos\AnalyzeScan\LIMPFileReader\xsd\mzXML_idx_2.1.xsd" workingxmlfile="C:\Users\den\temp\analysis3_5.mzXML"/>
			<xsdschemasource name="XML" main="1" schemafile="C:\Users\den\source\repos\AnalyzeScan\LIMPFileReader\xsd\mzXML_2.1.xsd" workingxmlfile="C:\Users\den\temp\analysis3_5.mzXML"/>
		</schemasources>
	</schemasources>
	<modules/>
	<flags>
		<scripts/>
		<mainparts/>
		<globalparts/>
		<designfragments/>
		<pagelayouts/>
		<xpath-functions/>
	</flags>
	<scripts>
		<script language="javascript"/>
	</scripts>
	<script-project>
		<Project version="4" app="AuthenticView"/>
	</script-project>
	<importedxslt/>
	<globalstyles/>
	<mainparts>
		<children>
			<globaltemplate subtype="main" match="/">
				<document-properties/>
				<children>
					<documentsection>
						<properties columncount="1" columngap="0.50in" headerfooterheight="fixed" pagemultiplepages="0" pagenumberingformat="1" pagenumberingstartat="auto" pagestart="next" paperheight="11in" papermarginbottom="0.79in" papermarginfooter="0.30in" papermarginheader="0.30in" papermarginleft="0.60in" papermarginright="0.60in" papermargintop="0.79in" paperwidth="8.50in"/>
						<watermark>
							<image transparency="50" fill-page="1" center-if-not-fill="1"/>
							<text transparency="50"/>
						</watermark>
					</documentsection>
					<text fixtext="Report for: "/>
					<template subtype="source" match="XML">
						<children>
							<template subtype="element" match="cff:msRun">
								<children>
									<template subtype="element" match="cff:parentFile">
										<children>
											<template subtype="attribute" match="fileName">
												<children>
													<paragraph paragraphtag="p">
														<children>
															<content subtype="regular"/>
														</children>
													</paragraph>
												</children>
												<variables/>
											</template>
										</children>
										<variables/>
									</template>
								</children>
								<variables/>
							</template>
						</children>
						<variables/>
					</template>
					<newline/>
					<text fixtext="Comments:"/>
					<newline/>
					<template subtype="source" match="XML">
						<children>
							<template subtype="element" match="cff:msRun">
								<children>
									<template subtype="element" match="cff:dataProcessing">
										<children>
											<template subtype="element" match="cff:comment">
												<children>
													<paragraph paragraphtag="p">
														<children>
															<content subtype="regular"/>
														</children>
													</paragraph>
												</children>
												<variables/>
											</template>
										</children>
										<variables/>
									</template>
								</children>
								<variables/>
							</template>
						</children>
						<variables/>
					</template>
					<newline/>
					<text fixtext="&lt;h2&gt;Analysis details&lt;/h2&gt;"/>
					<newline/>
					<tgrid tablegen-filter-periods-to-month="12" tablegen-filter-periods-to-day="31">
						<properties border="1" width="100%"/>
						<children>
							<tgridbody-cols>
								<children>
									<tgridcol>
										<styles width="1.79in"/>
									</tgridcol>
									<tgridcol/>
								</children>
							</tgridbody-cols>
							<tgridbody-rows>
								<children>
									<tgridrow>
										<children>
											<tgridcell>
												<children>
													<text fixtext="Scan start time"/>
												</children>
											</tgridcell>
											<tgridcell>
												<children>
													<template subtype="source" match="XML">
														<children>
															<template subtype="element" match="cff:msRun">
																<children>
																	<template subtype="attribute" match="startTime">
																		<children>
																			<content subtype="regular">
																				<format basic-type="xsd" datatype="duration"/>
																			</content>
																		</children>
																		<variables/>
																	</template>
																</children>
																<variables/>
															</template>
														</children>
														<variables/>
													</template>
												</children>
											</tgridcell>
										</children>
									</tgridrow>
									<tgridrow>
										<children>
											<tgridcell>
												<children>
													<text fixtext="Scan end time"/>
												</children>
											</tgridcell>
											<tgridcell>
												<children>
													<template subtype="source" match="XML">
														<children>
															<template subtype="element" match="cff:msRun">
																<children>
																	<template subtype="attribute" match="endTime">
																		<children>
																			<content subtype="regular">
																				<format basic-type="xsd" datatype="duration"/>
																			</content>
																		</children>
																		<variables/>
																	</template>
																</children>
																<variables/>
															</template>
														</children>
														<variables/>
													</template>
												</children>
											</tgridcell>
										</children>
									</tgridrow>
									<tgridrow>
										<children>
											<tgridcell>
												<children>
													<text fixtext="Count of scans"/>
												</children>
											</tgridcell>
											<tgridcell>
												<children>
													<template subtype="source" match="XML">
														<children>
															<template subtype="element" match="cff:msRun">
																<children>
																	<template subtype="attribute" match="scanCount">
																		<children>
																			<content subtype="regular">
																				<format basic-type="xsd" datatype="positiveInteger"/>
																			</content>
																		</children>
																		<variables/>
																	</template>
																</children>
																<variables/>
															</template>
														</children>
														<variables/>
													</template>
												</children>
											</tgridcell>
										</children>
									</tgridrow>
								</children>
							</tgridbody-rows>
						</children>
						<wizard-data-repeat>
							<children/>
						</wizard-data-repeat>
						<wizard-data-rows>
							<children/>
						</wizard-data-rows>
						<wizard-data-columns>
							<children/>
						</wizard-data-columns>
					</tgrid>
					<newline/>
					<text fixtext="&lt;h2&gt;Scans details&lt;/h2&gt;"/>
					<newline/>
					<list ordered="1">
						<children>
							<template subtype="source" match="XML">
								<children>
									<template subtype="element" match="cff:msRun">
										<children>
											<template subtype="element" match="cff:scan">
												<children>
													<listrow>
														<children>
															<content subtype="regular"/>
														</children>
													</listrow>
												</children>
												<variables/>
											</template>
										</children>
										<variables/>
									</template>
								</children>
								<variables/>
							</template>
						</children>
					</list>
					<newline/>
					<newline/>
					<text fixtext="&lt;h2&gt;Instrument details&lt;/h2&gt;"/>
					<newline/>
					<tgrid tablegen-filter-periods-to-month="12" tablegen-filter-periods-to-day="31">
						<properties border="1" width="100%"/>
						<children>
							<tgridbody-cols>
								<children>
									<tgridcol>
										<styles width="1.79in"/>
									</tgridcol>
									<tgridcol/>
								</children>
							</tgridbody-cols>
							<tgridbody-rows>
								<children>
									<tgridrow>
										<children>
											<tgridcell>
												<children>
													<text fixtext="Ionization"/>
												</children>
											</tgridcell>
											<tgridcell>
												<children>
													<template subtype="source" match="XML">
														<children>
															<template subtype="element" match="cff:msRun">
																<children>
																	<template subtype="element" match="cff:msInstrument">
																		<children>
																			<template subtype="element" match="cff:msIonisation">
																				<children>
																					<template subtype="attribute" match="value">
																						<children>
																							<content subtype="regular"/>
																						</children>
																						<variables/>
																					</template>
																				</children>
																				<variables/>
																			</template>
																		</children>
																		<variables/>
																	</template>
																</children>
																<variables/>
															</template>
														</children>
														<variables/>
													</template>
												</children>
											</tgridcell>
										</children>
									</tgridrow>
									<tgridrow>
										<children>
											<tgridcell>
												<children>
													<text fixtext="Analyzer"/>
												</children>
											</tgridcell>
											<tgridcell>
												<children>
													<template subtype="source" match="XML">
														<children>
															<template subtype="element" match="cff:msRun">
																<children>
																	<template subtype="element" match="cff:msInstrument">
																		<children>
																			<template subtype="element" match="cff:msMassAnalyzer">
																				<children>
																					<template subtype="attribute" match="value">
																						<children>
																							<content subtype="regular"/>
																						</children>
																						<variables/>
																					</template>
																				</children>
																				<variables/>
																			</template>
																		</children>
																		<variables/>
																	</template>
																</children>
																<variables/>
															</template>
														</children>
														<variables/>
													</template>
												</children>
											</tgridcell>
										</children>
									</tgridrow>
								</children>
							</tgridbody-rows>
						</children>
						<wizard-data-repeat>
							<children/>
						</wizard-data-repeat>
						<wizard-data-rows>
							<children/>
						</wizard-data-rows>
						<wizard-data-columns>
							<children/>
						</wizard-data-columns>
					</tgrid>
					<newline/>
				</children>
			</globaltemplate>
		</children>
	</mainparts>
	<globalparts/>
	<designfragments/>
	<xmltables>
		<children>
			<xmltable type="HTML">
				<children>
					<xmltable-tag tag-name="cff:comment"/>
					<xmltable-tag tag-type="Caption"/>
					<xmltable-tag tag-type="Header"/>
					<xmltable-tag tag-type="Footer"/>
					<xmltable-tag tag-type="Body"/>
					<xmltable-tag tag-type="Row" tag-name="tr"/>
					<xmltable-tag tag-type="Cell" tag-name="td"/>
					<xmltable-tag tag-type="HeaderCell"/>
				</children>
			</xmltable>
		</children>
	</xmltables>
	<authentic-custom-toolbar-buttons/>
</structure>
