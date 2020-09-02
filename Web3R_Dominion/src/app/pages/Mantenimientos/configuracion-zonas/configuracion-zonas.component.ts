
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { RespuestaServer } from '../../../models/respuestaServer.models';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import { from, combineLatest } from 'rxjs';
import Swal from 'sweetalert2';
import { OrdenTrabajoService } from '../../../services/Procesos/orden-trabajo.service';
 

declare var $:any;

@Component({
  selector: 'app-configuracion-zonas',
  templateUrl: './configuracion-zonas.component.html',
  styleUrls: ['./configuracion-zonas.component.css']
})
 

export class ConfiguracionZonasComponent implements OnInit {

  formParamsFiltro : FormGroup;
  idUserGlobal :number = 0;
  proveedor :any [] =[];
 
   constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService,private funcionGlobalServices : FuncionesglobalesService, private funcionesglobalesService : FuncionesglobalesService, private ordenTrabajoService : OrdenTrabajoService ) {         
     this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
 
   this.getCargarCombos();
   this.inicializarFormularioFiltro();
 }

 inicializarFormularioFiltro(){ 
    this.formParamsFiltro= new FormGroup({
      idEstado : new FormControl('0')
     }) 
 }
  
 getCargarCombos(){ 
    this.spinner.show();

    combineLatest([ this.ordenTrabajoService.get_Proveedor() ])
    .subscribe(([_proveedor])=>{
      
      this.spinner.hide();  
      this.proveedor = _proveedor; 
    })

 
 }  
  
 cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('hide');  
    },0); 
 }

 
 

}

