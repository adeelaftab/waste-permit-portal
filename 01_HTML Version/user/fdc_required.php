<? include('include/header.php'); ?>
  
  <!-- ============================================================== --> 
  <!-- Start right Content here --> 
  <!-- ============================================================== -->
  <div class="content-page"> 
    <!-- Start content -->
    <div class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-sm-12">
            <div class="page-title-box">
              <h4 class="page-title">FDC Management</h4>
              <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashoard</a></li>
                <li class="breadcrumb-item"><a href="javascript:void(0);">FDC</a></li>
              </ol>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-8">
            <div id="smartwizard">
              <ul>
                <li><a href="#step-1"><i style="font-size: 40px" class="fa fa-info-circle"></i><br />
                  <small>Company Information</small></a></li>
                <li><a href="#step-2"><i style="font-size: 40px" class="fa fa-prescription-bottle "></i><br />
                  <small>Waste Description</small></a></li>
                <li><a href="#step-3"><i style="font-size: 40px" class="fa fa-sitemap "></i><br />
                  <small>Submission Approval</small></a></li>
                <li><a href="#step-4"><i style="font-size: 40px" class="fa fa-shopping-cart"></i><br />
                  <small>Summary</small></a></li>
              </ul>
              <div>
                <div id="step-1" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                        <h6 class="wizard-title">Please fill your comapny information below:</h6>
                        <hr>
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Company Name</label>
                        <input class="form-control"  value="PLAN A Agency" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Trade License Number</label>
                        <input class="form-control"  value="773330" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Manager Name</label>
                        <input class="form-control"  value="jarkas" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Phone</label>
                        <input class="form-control"  value="123456" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Email</label>
                        <input class="form-control"  value="info@plana.ae" type="text">
                      </div>
                    </div>
                  </div>
                </div>
                <div id="step-2" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                        <h6 class="wizard-title">Please fill the waste description information below:</h6>
                        <hr>
                      </div>
                   
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <h6 class="wizard-title">List of items</h6>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <div class="row categorybxesbg">
                            
                            
                          <div class="col-sm-12">
                            <div class="btn-group mo-mb-2 wt-btn">
                                                <button class="btn btn-secondary btn-lg dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Select Items
                                                </button>
                                                <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 41px, 0px);">
                                                    <a class="dropdown-item" href="#">Municipal Solid Waste</a>
                                                    <a class="dropdown-item" href="#">Industrial Non-hazardous Waste</a>
                                                    <a class="dropdown-item" href="#">Hazardous Waste</a>
                                                    <a class="dropdown-item" href="#">Expired Chemical/Products</a>
                                                    <a class="dropdown-item" href="#">Lab Analysis & Sampling</a>
                                                </div>
                                            </div>
                            </div>
                        </div>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12">
                        <hr>
                        <br>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Country of Origin</label>
                            <div class="">
              <div class="ui-select">
					<select id="user_time_zone" class="form-control">
					<option value="Afganistan">Afghanistan</option>
					<option value="Albania">Albania</option>
					<option value="Algeria">Algeria</option>
					<option value="American Samoa">American Samoa</option>
					<option value="Andorra">Andorra</option>
					<option value="Angola">Angola</option>
					<option value="Anguilla">Anguilla</option>
					<option value="Antigua & Barbuda">Antigua & Barbuda</option>
					<option value="Argentina">Argentina</option>
					<option value="Armenia">Armenia</option>
					<option value="Aruba">Aruba</option>
					<option value="Australia">Australia</option>
					<option value="Austria">Austria</option>
					<option value="Azerbaijan">Azerbaijan</option>
					<option value="Bahamas">Bahamas</option>
					<option value="Bahrain">Bahrain</option>
					<option value="Bangladesh">Bangladesh</option>
					<option value="Barbados">Barbados</option>
					<option value="Belarus">Belarus</option>
					<option value="Belgium">Belgium</option>
					<option value="Belize">Belize</option>
					<option value="Benin">Benin</option>
					<option value="Bermuda">Bermuda</option>
					<option value="Bhutan">Bhutan</option>
					<option value="Bolivia">Bolivia</option>
					<option value="Bonaire">Bonaire</option>
					<option value="Bosnia & Herzegovina">Bosnia & Herzegovina</option>
					<option value="Botswana">Botswana</option>
					<option value="Brazil">Brazil</option>
					<option value="British Indian Ocean Ter">British Indian Ocean Ter</option>
					<option value="Brunei">Brunei</option>
					<option value="Bulgaria">Bulgaria</option>
					<option value="Burkina Faso">Burkina Faso</option>
					<option value="Burundi">Burundi</option>
					<option value="Cambodia">Cambodia</option>
					<option value="Cameroon">Cameroon</option>
					<option value="Canada">Canada</option>
					<option value="Canary Islands">Canary Islands</option>
					<option value="Cape Verde">Cape Verde</option>
					<option value="Cayman Islands">Cayman Islands</option>
					<option value="Central African Republic">Central African Republic</option>
					<option value="Chad">Chad</option>
					<option value="Channel Islands">Channel Islands</option>
					<option value="Chile">Chile</option>
					<option value="China">China</option>
					<option value="Christmas Island">Christmas Island</option>
					<option value="Cocos Island">Cocos Island</option>
					<option value="Colombia">Colombia</option>
					<option value="Comoros">Comoros</option>
					<option value="Congo">Congo</option>
					<option value="Cook Islands">Cook Islands</option>
					<option value="Costa Rica">Costa Rica</option>
					<option value="Cote DIvoire">Cote DIvoire</option>
					<option value="Croatia">Croatia</option>
					<option value="Cuba">Cuba</option>
					<option value="Curaco">Curacao</option>
					<option value="Cyprus">Cyprus</option>
					<option value="Czech Republic">Czech Republic</option>
					<option value="Denmark">Denmark</option>
					<option value="Djibouti">Djibouti</option>
					<option value="Dominica">Dominica</option>
					<option value="Dominican Republic">Dominican Republic</option>
					<option value="East Timor">East Timor</option>
					<option value="Ecuador">Ecuador</option>
					<option value="Egypt">Egypt</option>
					<option value="El Salvador">El Salvador</option>
					<option value="Equatorial Guinea">Equatorial Guinea</option>
					<option value="Eritrea">Eritrea</option>
					<option value="Estonia">Estonia</option>
					<option value="Ethiopia">Ethiopia</option>
					<option value="Falkland Islands">Falkland Islands</option>
					<option value="Faroe Islands">Faroe Islands</option>
					<option value="Fiji">Fiji</option>
					<option value="Finland">Finland</option>
					<option value="France">France</option>
					<option value="French Guiana">French Guiana</option>
					<option value="French Polynesia">French Polynesia</option>
					<option value="French Southern Ter">French Southern Ter</option>
					<option value="Gabon">Gabon</option>
					<option value="Gambia">Gambia</option>
					<option value="Georgia">Georgia</option>
					<option value="Germany">Germany</option>
					<option value="Ghana">Ghana</option>
					<option value="Gibraltar">Gibraltar</option>
					<option value="Great Britain">Great Britain</option>
					<option value="Greece">Greece</option>
					<option value="Greenland">Greenland</option>
					<option value="Grenada">Grenada</option>
					<option value="Guadeloupe">Guadeloupe</option>
					<option value="Guam">Guam</option>
					<option value="Guatemala">Guatemala</option>
					<option value="Guinea">Guinea</option>
					<option value="Guyana">Guyana</option>
					<option value="Haiti">Haiti</option>
					<option value="Hawaii">Hawaii</option>
					<option value="Honduras">Honduras</option>
					<option value="Hong Kong">Hong Kong</option>
					<option value="Hungary">Hungary</option>
					<option value="Iceland">Iceland</option>
					<option value="Indonesia">Indonesia</option>
					<option value="India">India</option>
					<option value="Iran">Iran</option>
					<option value="Iraq">Iraq</option>
					<option value="Ireland">Ireland</option>
					<option value="Isle of Man">Isle of Man</option>
					<option value="Israel">Israel</option>
					<option value="Italy">Italy</option>
					<option value="Jamaica">Jamaica</option>
					<option value="Japan">Japan</option>
					<option value="Jordan">Jordan</option>
					<option value="Kazakhstan">Kazakhstan</option>
					<option value="Kenya">Kenya</option>
					<option value="Kiribati">Kiribati</option>
					<option value="Korea North">Korea North</option>
					<option value="Korea Sout">Korea South</option>
					<option value="Kuwait">Kuwait</option>
					<option value="Kyrgyzstan">Kyrgyzstan</option>
					<option value="Laos">Laos</option>
					<option value="Latvia">Latvia</option>
					<option value="Lebanon">Lebanon</option>
					<option value="Lesotho">Lesotho</option>
					<option value="Liberia">Liberia</option>
					<option value="Libya">Libya</option>
					<option value="Liechtenstein">Liechtenstein</option>
					<option value="Lithuania">Lithuania</option>
					<option value="Luxembourg">Luxembourg</option>
					<option value="Macau">Macau</option>
					<option value="Macedonia">Macedonia</option>
					<option value="Madagascar">Madagascar</option>
					<option value="Malaysia">Malaysia</option>
					<option value="Malawi">Malawi</option>
					<option value="Maldives">Maldives</option>
					<option value="Mali">Mali</option>
					<option value="Malta">Malta</option>
					<option value="Marshall Islands">Marshall Islands</option>
					<option value="Martinique">Martinique</option>
					<option value="Mauritania">Mauritania</option>
					<option value="Mauritius">Mauritius</option>
					<option value="Mayotte">Mayotte</option>
					<option value="Mexico">Mexico</option>
					<option value="Midway Islands">Midway Islands</option>
					<option value="Moldova">Moldova</option>
					<option value="Monaco">Monaco</option>
					<option value="Mongolia">Mongolia</option>
					<option value="Montserrat">Montserrat</option>
					<option value="Morocco">Morocco</option>
					<option value="Mozambique">Mozambique</option>
					<option value="Myanmar">Myanmar</option>
					<option value="Nambia">Nambia</option>
					<option value="Nauru">Nauru</option>
					<option value="Nepal">Nepal</option>
					<option value="Netherland Antilles">Netherland Antilles</option>
					<option value="Netherlands">Netherlands (Holland, Europe)</option>
					<option value="Nevis">Nevis</option>
					<option value="New Caledonia">New Caledonia</option>
					<option value="New Zealand">New Zealand</option>
					<option value="Nicaragua">Nicaragua</option>
					<option value="Niger">Niger</option>
					<option value="Nigeria">Nigeria</option>
					<option value="Niue">Niue</option>
					<option value="Norfolk Island">Norfolk Island</option>
					<option value="Norway">Norway</option>
					<option value="Oman">Oman</option>
					<option value="Pakistan">Pakistan</option>
					<option value="Palau Island">Palau Island</option>
					<option value="Palestine">Palestine</option>
					<option value="Panama">Panama</option>
					<option value="Papua New Guinea">Papua New Guinea</option>
					<option value="Paraguay">Paraguay</option>
					<option value="Peru">Peru</option>
					<option value="Phillipines">Philippines</option>
					<option value="Pitcairn Island">Pitcairn Island</option>
					<option value="Poland">Poland</option>
					<option value="Portugal">Portugal</option>
					<option value="Puerto Rico">Puerto Rico</option>
					<option value="Qatar">Qatar</option>
					<option value="Republic of Montenegro">Republic of Montenegro</option>
					<option value="Republic of Serbia">Republic of Serbia</option>
					<option value="Reunion">Reunion</option>
					<option value="Romania">Romania</option>
					<option value="Russia">Russia</option>
					<option value="Rwanda">Rwanda</option>
					<option value="St Barthelemy">St Barthelemy</option>
					<option value="St Eustatius">St Eustatius</option>
					<option value="St Helena">St Helena</option>
					<option value="St Kitts-Nevis">St Kitts-Nevis</option>
					<option value="St Lucia">St Lucia</option>
					<option value="St Maarten">St Maarten</option>
					<option value="St Pierre & Miquelon">St Pierre & Miquelon</option>
					<option value="St Vincent & Grenadines">St Vincent & Grenadines</option>
					<option value="Saipan">Saipan</option>
					<option value="Samoa">Samoa</option>
					<option value="Samoa American">Samoa American</option>
					<option value="San Marino">San Marino</option>
					<option value="Sao Tome & Principe">Sao Tome & Principe</option>
					<option value="Saudi Arabia">Saudi Arabia</option>
					<option value="Senegal">Senegal</option>
					<option value="Seychelles">Seychelles</option>
					<option value="Sierra Leone">Sierra Leone</option>
					<option value="Singapore">Singapore</option>
					<option value="Slovakia">Slovakia</option>
					<option value="Slovenia">Slovenia</option>
					<option value="Solomon Islands">Solomon Islands</option>
					<option value="Somalia">Somalia</option>
					<option value="South Africa">South Africa</option>
					<option value="Spain">Spain</option>
					<option value="Sri Lanka">Sri Lanka</option>
					<option value="Sudan">Sudan</option>
					<option value="Suriname">Suriname</option>
					<option value="Swaziland">Swaziland</option>
					<option value="Sweden">Sweden</option>
					<option value="Switzerland">Switzerland</option>
					<option value="Syria">Syria</option>
					<option value="Tahiti">Tahiti</option>
					<option value="Taiwan">Taiwan</option>
					<option value="Tajikistan">Tajikistan</option>
					<option value="Tanzania">Tanzania</option>
					<option value="Thailand">Thailand</option>
					<option value="Togo">Togo</option>
					<option value="Tokelau">Tokelau</option>
					<option value="Tonga">Tonga</option>
					<option value="Trinidad & Tobago">Trinidad & Tobago</option>
					<option value="Tunisia">Tunisia</option>
					<option value="Turkey">Turkey</option>
					<option value="Turkmenistan">Turkmenistan</option>
					<option value="Turks & Caicos Is">Turks & Caicos Is</option>
					<option value="Tuvalu">Tuvalu</option>
					<option value="Uganda">Uganda</option>
					<option value="United Kingdom">United Kingdom</option>
					<option value="Ukraine">Ukraine</option>
					<option selected value="United Arab Erimates">United Arab Emirates</option>
					<option value="United States of America">United States of America</option>
					<option value="Uraguay">Uruguay</option>
					<option value="Uzbekistan">Uzbekistan</option>
					<option value="Vanuatu">Vanuatu</option>
					<option value="Vatican City State">Vatican City State</option>
					<option value="Venezuela">Venezuela</option>
					<option value="Vietnam">Vietnam</option>
					<option value="Virgin Islands (Brit)">Virgin Islands (Brit)</option>
					<option value="Virgin Islands (USA)">Virgin Islands (USA)</option>
					<option value="Wake Island">Wake Island</option>
					<option value="Wallis & Futana Is">Wallis & Futana Is</option>
					<option value="Yemen">Yemen</option>
					<option value="Zaire">Zaire</option>
					<option value="Zambia">Zambia</option>
					<option value="Zimbabwe">Zimbabwe</option>
					</select>
              </div>
            </div> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Total Weight (kg\ton)</label>
                            <input class="form-control text-box single-line" data-val="true" data-val-maxlength="Total Weight / Volume cannot be longer than 10 characters" data-val-maxlength-max="10" data-val-minlength="The field TotalVolume must be a string or array type with a minimum length of '0'." data-val-minlength-min="0" data-val-regex="Total Weight / Volume must be numeric" data-val-regex-pattern="^[0-9]*$" data-val-required="Total Weight / Volume is required" id="TotalVolume" maxlength="10" name="TotalVolume" placeholder="Total Weight (kg\ton)" value="" type="text">
                            <div class="btn-group m-b-10 uom">
                              <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Unit of Measure</button>
                              <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 33px, 0px);"> <a class="dropdown-item" href="#">KG</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#">TON</a> </div>
                            </div>
                            <span class="field-validation-valid text-danger" data-valmsg-for="TotalVolume" data-valmsg-replace="true"></span> </div>
                        </div>
                          
                      </div>
                        
                         <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12">
                             <hr><br></div>
                        </div>
                      <div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label class="control-label" for="SourceProcess">PRO</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>EXP Date</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label class="control-label" for="SourceProcess">Type of packaging</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>No of Pakages</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
					<div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                             <label>Package size (kg\ton)</label>
                            <input class="form-control text-box single-line" data-val="true" data-val-maxlength="Total Weight / Volume cannot be longer than 10 characters" data-val-maxlength-max="10" data-val-minlength="The field TotalVolume must be a string or array type with a minimum length of '0'." data-val-minlength-min="0" data-val-regex="Total Weight / Volume must be numeric" data-val-regex-pattern="^[0-9]*$" data-val-required="Total Weight / Volume is required" id="TotalVolume" maxlength="10" name="TotalVolume" placeholder="Total Weight (kg\ton)" value="" type="text">
                            <div class="btn-group m-b-10 uom" style="bottom: 10px;">
                              <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Unit of Measure</button>
                              <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 33px, 0px);"> <a class="dropdown-item" href="#">KG</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#">TON</a> </div>
                            </div>
							  
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            
							  <div class="row">
								  
								<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <h6 class="wizard-title" style="margin: 0;">Reason for Destruction</h6>
                      </div>
								  <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
								  <div class="btn-group mo-mb-2 wt-btn">
                                                <button class="btn btn-secondary btn-lg dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Reason for Destruction
                                                </button>
                                                <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 41px, 0px);">
                                                    <a class="dropdown-item" href="#">Expired food items</a>
                                                    <a class="dropdown-item" href="#">Perishable goods</a>
                                                </div>
                                            </div>
								  </div>
							  </div>
							</div>
                        </div>
                      </div>
						
						<div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label class="control-label" for="SourceProcess">HFZ inspection form</label>
							<div class="field user-name">
                            <input class="form-control"  value="" type="text">
                            <div class="uploaddocument-btn"><i class="fa fa-upload"></i><span>Attach HFZ inspection form</span></div>
                            <span class="validate-img"></span>
                        </div>
							  
							</div>
                          
							<div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Location of facility</label>
                            <input autofocus="geolocate()" class="form-control text-box single-line" data-val="true" data-val-length="Waste Location cannot be longer than 200 characters" data-val-length-max="200" data-val-required="Waste Location is required" id="WasteLocation" maxlength="10" name="WasteLocation" placeholder="Auto Detect Current Location" value="" autocomplete="off" type="text">
                            <div class="uploaddocument-btn openthemap"><i class="ti-target"></i><span>Open the map</span></div>
                            <span class="field-validation-valid text-danger" data-valmsg-for="WasteLocation" data-valmsg-replace="true"></span> </div>
							
                        </div>
							
							<div class="row detectlocation">
                      <div class="col-xs-12 col-sm-12 col-md-12">
                              <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d14424.367557157131!2d55.3885906!3d25.3346974!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0xfecbbba84b50be74!2sBee&#39;ah%20Head%20Office!5e0!3m2!1sen!2sae!4v1570813869235!5m2!1sen!2sae" width="100%" height="450" frameborder="0" style="border:0;" allowfullscreen=""></iframe>
                              </div>
                          </div>
							
                      </div>
						
                    </div>
                  </div>
                </div>
                <div id="step-3" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
						  <h6 class="wizard-title" style="background: #eee; padding: 20px;"><center><strong>Hazardous waste contract</strong></center></h6>
                      </div>
						
                      <div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label class="control-label" for="SourceProcess">Waste Disposal Permit No</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Transporter</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
						  <h6 class="wizard-title" style="background: #eee; padding: 20px;"><center><strong>Waste Details</strong></center></h6>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 form-group">
                            <label class="control-label" for="SourceProcess">Name</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 form-group">
                            <label>Quantity</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 form-group">
                            <label>State</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 form-group">
                            <label>Container Type</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
					  <div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                            <label class="control-label" for="SourceProcess">Description of Waste Generation</label>
							  <textarea class="form-control"  value="" type="text" style="height: 150px; border: 1px solid #f5f5f5;"></textarea>
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
					  <div class="col-xs-12 col-sm-12 col-md-12" style="margin-top: 50px;">
                        <div class="row">
                          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                            <label class="control-label" for="SourceProcess">Frequency of Disposal</label>
                            <input class="form-control"  value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>


                    </div>
                  </div>
                </div>
                <div id="step-4" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                        <h6 class="wizard-title">Please review your submitted information below:</h6>
                        <hr>
                      </div>
                      <div class="col-lg-12">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges summary">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Company Information</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> <p>Company Name</p></th>
                                    <td>Plan A</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i> <p>Trade License Number</p></th>
                                    <td>773330</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i> <p>Manager Name</p></th>
                                    <td>jarkas</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i> <p>Phone</p></th>
                                    <td>123456</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i> <p>Email</p></th>
                                    <td>info@plana.ae</td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      </div>
                      </div>
                      <div class="row">
                      <hr>
                    </div>
                      <div class="row">
                      <div class="col-lg-12">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges summary">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <p><span>Waste Description</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> <p>List of Items</p></th>
                                    <td>Municipal Solid Waste</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i><p>Country of Origin</p></th>
                                    <td>United Arab Emirates</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i><p>Total Weight (kg\ton)</p></th>
                                    <td>500 KG</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i><p>PRO</p></th>
                                    <td>4802938429</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>EXP Date</p></th>
                                    <td>30 / 10 / 2019</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Type of packaging</p></th>
                                    <td>Lorem Ipsum</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>No of Pakages</p></th>
                                    <td>657</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Package Size</p></th>
                                    <td>657</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Reason for Destruction</p></th>
                                    <td>Expired food items</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>HFZ inspection form</p></th>
                                    <td>HFZ IF.pdf</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Location of facility</p></th>
                                    <td><a href="https://goo.gl/maps/hfBWQFGYEZ9gjwq59">Open the Map</a></td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    
                    
                    <div class="row">
                      <hr>
                    </div>
                    <div class="row">
                      <div class="col-md-12 col-xs-12 form-group">
                        <div class="checkbox">
                          <label>
                            <input style="margin: 0 10px 10px 0; float: left;" data-val="true" data-val-range="You must agree with the Disclaimer" data-val-range-max="True" data-val-range-min="True" data-val-required="The Disclaimer field is required." id="IsDisclaimerCertified" name="IsDisclaimerCertified" value="true" type="checkbox">
                            <input name="IsDisclaimerCertified" value="false" type="hidden">
                            I certify that my answers are true and complete to the best of my knowledge <span class="field-validation-valid text-danger" data-valmsg-for="IsDisclaimerCertified" data-valmsg-replace="true"></span> </label>
                        </div>
                        <div class="checkbox">
                          <label>
                            <input style="margin: 0 10px 10px 0; float: left;" data-val="true" data-val-range="You must agree with the Terms and Conditions" data-val-range-max="True" data-val-range-min="True" data-val-required="Disclaimer is required" id="IsIAgreeTermsAndCondition" name="IsIAgreeTermsAndCondition" value="true" type="checkbox">
                            <input name="IsIAgreeTermsAndCondition" value="false" type="hidden">
                            I agree to the Terms and Conditions. <span class="field-validation-valid text-danger" data-valmsg-for="IsIAgreeTermsAndCondition" data-valmsg-replace="true"></span> </label>
                        </div>
                      </div>
                      <div class="col-md-12 col-xs-12 form-group"></div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Name</label>
                        <input class="form-control text-box single-line" data-val="true" data-val-length="Name cannot be longer than 200 characters" data-val-length-max="200" data-val-regex="Use characters only" data-val-regex-pattern="[A-Za-z ]*" data-val-required="Disclaimer Name is required" id="DisclaimerName" name="DisclaimerName" value="" type="text">
                        <span class="field-validation-valid text-danger" data-valmsg-for="DisclaimerName" data-valmsg-replace="true"></span> </div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Date</label>
                        <label class="form-control" for="">30/09/2018</label>
                      </div>
                    </div>
                    <div class="row checkoutbtn">
                      <button type="button" class="btn btn-success btn-lg waves-effect checkoutbtn" style="margin: 0 auto;">Check Out</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="creditcard">
              <iframe width="100%" height="550px" src="include/card/index.html" frameborder="0"></iframe>
              <div class="col-sm-12" style="background: #f9f9f9; margin-top: -10px; padding-bottom: 30px;">
                <div class="button-items" style="display: table; margin: 0 auto;">
                  <button type="button" class="btn btn-secondary waves-effect cancelcheckout">Back</button>
                  <a href="invoice.php">
                  <button type="button" class="btn btn-success waves-effect waves-light submitcheckout">Submit</button>
                  </a> </div>
              </div>
            </div>
          </div>
          <div class="col-lg-4">
            <div class="card" style="min-height: 100%;">
              <div class="card-body charges">
                <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Charges</span></h4>
                <div class="table-responsive">
                  <table class="table mb-0">
                    <tbody>
                      <tr>
                        <th scope="row"><i class="ti-zip charges-icons"></i> Permit Fees</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-settings charges-icons"></i> Service Fees</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-ruler-pencil charges-icons"></i> R&amp;D Fees</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-bookmark-alt charges-icons"></i> VAT</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr class="totalcharges">
                        <th scope="row"><i class="ti-files charges-icons"></i> Total</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
          <!-- end col --> 
        </div>
        <!-- end row --> 
      </div>
      <!-- container-fluid --> 
    </div>
    <!-- content -->
    
    <? include('include/footer.php'); ?>
