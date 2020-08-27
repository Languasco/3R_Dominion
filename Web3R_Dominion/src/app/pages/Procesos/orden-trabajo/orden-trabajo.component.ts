
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { RespuestaServer } from '../../../models/respuestaServer.models';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import { from, combineLatest } from 'rxjs';
import Swal from 'sweetalert2';
 
import { ListaPreciosService } from '../../../services/Mantenimientos/lista-precios.service';
import { OrdenTrabajoService } from '../../../services/Procesos/orden-trabajo.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { WebsocketService } from '../../../services/sockets/websocket.service';

declare var $:any;
@Component({
  selector: 'app-orden-trabajo',
  templateUrl: './orden-trabajo.component.html',
  styleUrls: ['./orden-trabajo.component.css']
})
 
export class OrdenTrabajoComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false

  servicios :any[]=[];   
  distritos :any[]=[];
  proveedor :any[]=[];   
  estados :any[]=[];   
  tipoOrdenTrabajo :any[]=[];
  jefeCuadrilla :any[]=[];
 
  ordenTrabajoCab :any[]=[]; 
  ordenTrabajoAsignacion :any[]=[]; 
  filtrarMantenimiento = "";
  opcionModal = "";
  tituloModal = "";

  tabControlDetalle: string[] = ['LISTA DE ORDENES','PUNTOS EN EL MAPA' ]; 
  selectedTabControlDetalle :any;

  checkeadoAll=false; 
  datepiekerConfig:Partial<BsDatepickerConfig>;
  totalGlobal =0;

  totalM3_empresa =0;
  totalM3_asignado =0;
  totalM3_pendiente =0;

  reporteResumen:any[]=[]; 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private listaPreciosService : ListaPreciosService, private ordenTrabajoService : OrdenTrabajoService, private funcionGlobalServices : FuncionesglobalesService, private websocketService : WebsocketService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
  this.selectedTabControlDetalle = this.tabControlDetalle[0];
   this.getCargarCombos();
   this.inicializarFormularioFiltro();
   this.inicializarFormulario_Asignacion();
 }

 selectTab(nameTab:any){
  this.selectedTabControlDetalle = nameTab 
 }

 inicializarFormularioFiltro(){ 
    this.formParamsFiltro= new FormGroup({
      idServicio : new FormControl('0'),
      idTipoOT : new FormControl('0'),
      idDistrito : new FormControl('0'),
      idProveedor : new FormControl('0'),
      idEstado : new FormControl('0')
     }) 
 }

 inicializarFormulario_Asignacion(){ 
 
    this.formParams = new FormGroup({
      fechaAsignacion: new FormControl(new Date()),
      empresa1: new FormControl('0'),
      jefeCuadrilla1: new FormControl('0'),
      empresa2: new FormControl('0'),
      jefeCuadrilla2: new FormControl('0'),
      observaciones: new FormControl(''),
    }) 
 } 
 getCargarCombos(){ 
    this.spinner.show();
    combineLatest([this.ordenTrabajoService.get_servicio(this.idUserGlobal), this.listaPreciosService.get_tipoOrdenTrabajo(), this.ordenTrabajoService.get_Distritos(), this.ordenTrabajoService.get_Proveedor(), this.ordenTrabajoService.get_estados(), this.ordenTrabajoService.get_jefeCuadrilla() ]).subscribe( ([ _servicios, _tipoOrdenTrabajo, _distritos, _proveedor,_estados,_jefeCuadrilla ])=>{
        this.servicios = _servicios;
        this.tipoOrdenTrabajo = _tipoOrdenTrabajo; 
        this.distritos = _distritos; 
        this.proveedor = _proveedor; 
        this.estados = _estados;
        this.jefeCuadrilla = _jefeCuadrilla;
      this.spinner.hide(); 
    },(error)=>{
      this.spinner.hide(); 
      alert(error);
    })

 }

 mostrarInformacion(){
      // if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
      //   return 
      // }
      
      // if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
      //   return 
      // }  

      // if (this.formParamsFiltro.value.idDistrito == '' || this.formParamsFiltro.value.idDistrito == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione un Distrito');
      //   return 
      // }

      // if (this.formParamsFiltro.value.idProveedor == '' || this.formParamsFiltro.value.idProveedor == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione un Proveedor');
      //   return 
      // }

      // if (this.formParamsFiltro.value.idEstado == '' || this.formParamsFiltro.value.idEstado == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione un Estado');
      //   return 
      // }
  
      this.checkeadoAll=false; 
      this.spinner.show();
      this.ordenTrabajoService.get_mostrarOrdenTrabajoCab_general(this.formParamsFiltro.value, this.idUserGlobal)
          .subscribe((res:RespuestaServer)=>{            
              this.spinner.hide();
              if (res.ok==true) {        
                  this.ordenTrabajoCab = res.data; 
              }else{
                this.alertasService.Swal_alert('error', JSON.stringify(res.data));
                alert(JSON.stringify(res.data));
              }
      })
 }   
  
 cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_OT').modal('hide');  
    },0); 
 }

 marcarTodos(){
  if (this.ordenTrabajoCab.length<=0) {
    return;
  }
  for (const obj of this.ordenTrabajoCab) {
    if (this.checkeadoAll) {
      obj.checkeado = false;
    }else{
      obj.checkeado = true;
    }
  }
 }

 validacionCheckMarcado(){    
  let CheckMarcado = false;
  // CheckMarcado = this.verificarCheckMarcado();
  CheckMarcado = this.funcionGlobalServices.verificarCheck_marcado(this.ordenTrabajoCab);

  if (CheckMarcado ==false) {
    this.alertasService.Swal_alert('error','Por favor debe marcar un elemento de la Tabla');
    return false;
  }else{
    return true;
  }
}

  obtnerCheckMarcado_opcion(opcionModal){
  
    let listRegistros =[]; 
    // listRegistros = this.ordenTrabajoCab.filter(ot => ot.checkeado &&  ( ot.id_Estado ==4 || ot.id_Estado == 6 || ot.id_Estado ==7 || ot.id_Estado ==23 ) ).map((obj)=>{
    listRegistros = this.ordenTrabajoCab.filter(ot => ot.checkeado).map((obj)=>{
      return {  id_OT : obj.id_OT, tipoOT : obj.tipoOT, nroObra: obj.nroObra, direccion : obj.direccion, distrito : obj.distrito  , empresaContratista : obj.empresaContratista, jefeCuadrilla : obj.jefeCuadrilla  , volumen : obj.volumen } ;
    });
  
  
    return listRegistros;
  }
  
  
   abrir_modalOT(opcionModal :string){
  
    if (this.validacionCheckMarcado()==false){
      return;
    } 
  
    this.opcionModal = opcionModal;
  
    if (opcionModal == 'Asignar') {    
        this.tituloModal = "ASIGNACION DE ORDENES DE TRABAJO";
    }else{    
        this.tituloModal = "REASIGNACION DE ORDENES DE TRABAJO";
    }
  
    this.ordenTrabajoAsignacion = [];
    this.ordenTrabajoAsignacion = this.obtnerCheckMarcado_opcion( this.opcionModal); 
  
    // if ( this.ordenTrabajoAsignacion.length ==0 ) {
    //   this.alertasService.Swal_alert('error','Por favor verifique o complete el proceso');
    //   return;
    // }    
      setTimeout(()=>{ // 
        $('#modal_OT').modal('show');
      },0); 
  
      this.inicializarFormulario_Asignacion();
      this.CalculototalGlobal();
   }
  
   guardar_AsignacionReasignacion_Ot(){
    if (this.ordenTrabajoAsignacion.length <=0) {
      this.alertasService.Swal_alert('error', 'No hay datos para almacenar.');
      return;
    }
  
    if (this.formParams.value.fechaAsignacion == '' || this.formParams.value.proyectista == 0 || this.formParams.value.fechaAsignacion == null)  {
      this.alertasService.Swal_alert('error', 'Por favor seleccione la fecha de asignacion.');
      return;
    } 
  
    if (this.opcionModal == 'Asignar') { // 
      if (this.formParams.value.empresa1 == '' || this.formParams.value.empresa1 == 0 || this.formParams.value.empresa1 == null)  {
        this.alertasService.Swal_alert('error', 'Por favor seleccione la empresa.');
        return;
      }
      if (this.formParams.value.jefeCuadrilla1 == '' || this.formParams.value.jefeCuadrilla1 == 0 || this.formParams.value.jefeCuadrilla1 == null)  {
        this.alertasService.Swal_alert('error', 'Por favor seleccione el jefe de cuadrilla.');
        return;
      } 
    } 
    if (this.opcionModal == 'Reasignar') { // 
      if (this.formParams.value.empresa2 == '' || this.formParams.value.empresa2 == 0 || this.formParams.value.empresa2 == null)  {
        this.alertasService.Swal_alert('error', 'Por favor seleccione la empresa.');
        return;
      }
      if (this.formParams.value.jefeCuadrilla2 == '' || this.formParams.value.jefeCuadrilla2 == 0 || this.formParams.value.jefeCuadrilla2 == null)  {
        this.alertasService.Swal_alert('error', 'Por favor seleccione el jefe de cuadrilla.');
        return;
      }
    } 
  
    const codigosIdOT = this.funcionGlobalServices.obtenerTodos_IdPrincipal(this.ordenTrabajoAsignacion,'id_OT'); 
    const fechaFormato = this.funcionGlobalServices.formatoFecha(this.formParams.value.fechaAsignacion); 
  
    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'
    })
    Swal.showLoading();
    this.ordenTrabajoService.save_asignacionReasignacion_Ot(codigosIdOT.join() ,this.opcionModal, this.formParams.value, fechaFormato, this.idUserGlobal ).subscribe((res:RespuestaServer)=>{
      Swal.close();
      if (res.ok) { 
         this.alertasService.Swal_Success("Proceso realizado correctamente..")
           //-----listando la informacion  
         this.mostrarInformacion();
  
          ////-- notificaciones Socket para el movil----        
          // if (this.opcionModal == 'P') { // proyectista
          //     // const dataOt = {
          //     //   id_usuario : this.idUserGlobal,
          //     //   id_proyectista : this.formParams.value.proyectista,
          //     //   array_id_ots : codigos,
          //     //   cadena_id_ots : codigos.join(),
          //     //   cant_ots : codigos.length
          //     // }   
              
          //     const dataOt = {
          //       cant_ot : codigos.length,
          //       mensaje : `Usted tiene  ${codigos.length} ot, asignada`,
          //       id_personal : this.formParams.value.proyectista,
          //       tipo : 'Proyectista'
          //     }  
  
          //     this.websocketService.NotificacionOT_WebSocket(dataOt)
          //     .then( (res:any) =>{
          //       if (res.ok==true) {
          //         console.log(res.data);
          //       }else{
          //         this.alertasService.Swal_alert('Error Socket', JSON.stringify(res.data));
          //         alert(JSON.stringify(res.data));
          //       }
          //     }).catch((error)=>{
          //       this.alertasService.Swal_alert('Error Socket', JSON.stringify(error));
          //       alert(JSON.stringify(error));
          //     })           
          // }
        ////-- Fin de notificaciones Socket para el movil----
  
  
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })
  }

  eliminarCheckMarcado(item:any){    
    var index = this.ordenTrabajoAsignacion.indexOf( item );
    this.ordenTrabajoAsignacion.splice( index, 1 );
    this.CalculototalGlobal();
  }

  CalculototalGlobal(){
    let totalG= 0;
    for (const valor of this.ordenTrabajoAsignacion) {
     totalG += Number(valor.volumen);
    }
  
    this.totalGlobal =  Number(totalG.toFixed(2))
  }

  changeCalculos_AsignacionReasignacion_Origen(opcion){
    this.calculo_AsignacionReasignacion_Ot(this.formParams.value.empresa1, this.formParams.value.jefeCuadrilla1 )  
  }
  
  changeCalculos_AsignacionReasignacion_Destino(opcion){
    this.calculo_AsignacionReasignacion_Ot(this.formParams.value.empresa2, this.formParams.value.jefeCuadrilla2 )  
  }
  
  calculo_AsignacionReasignacion_Ot(idEmpresa:number, idJefeCuadrilla:number){
    if (idEmpresa ==0 || idJefeCuadrilla == 0  ){
      this.totalM3_empresa =0;
      this.totalM3_asignado =0;
      this.totalM3_pendiente =0;
      return;
    }  
  
    this.spinner.show();
    this.ordenTrabajoService.get_calculos_asignarReasignar_Ot(idEmpresa, idJefeCuadrilla, this.opcionModal, this.idUserGlobal)
        .subscribe((res:RespuestaServer)=>{            
            this.spinner.hide();
            console.log(res.data)
            if (res.ok==true) {        
               if (res.data.length > 0) {
                this.totalM3_empresa =  res.data[0].totalM3_empresa;
                this.totalM3_asignado = res.data[0].totalM3_asignado;
                this.totalM3_pendiente = res.data[0].totalM3_pendiente;
               }else{
                this.totalM3_empresa =0;
                this.totalM3_asignado =0;
                this.totalM3_pendiente =0;
               }
            }else{
              this.alertasService.Swal_alert('error', JSON.stringify(res.data));
              alert(JSON.stringify(res.data));
            }
    })
  }
  
  cerrarModal_Resumen(){
    $('#modal_resumen').modal('hide');    
  }
  
  abrirModal_Resumen(){
  
          if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
          this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
          return 
        }
        
        if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
          this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
          return 
        }  
  
        if (this.formParamsFiltro.value.idDistrito == '' || this.formParamsFiltro.value.idDistrito == 0) {
          this.alertasService.Swal_alert('error','Por favor seleccione un Distrito');
          return 
        }
  
        if (this.formParamsFiltro.value.idProveedor == '' || this.formParamsFiltro.value.idProveedor == 0) {
          this.alertasService.Swal_alert('error','Por favor seleccione un Proveedor');
          return 
        }
  
        if (this.formParamsFiltro.value.idEstado == '' || this.formParamsFiltro.value.idEstado == 0) {
          this.alertasService.Swal_alert('error','Por favor seleccione un Estado');
          return 
        }
    
        this.spinner.show();
        this.ordenTrabajoService.get_resumenOT_proveedor(this.formParamsFiltro.value, this.idUserGlobal)
            .subscribe((res:RespuestaServer)=>{            
                this.spinner.hide();
                if (res.ok==true) {        
                  this.reporteResumen = res.data;
                  setTimeout(() => {
                   $('#modal_resumen').modal('show');
                 }, 100);
                }else{
                  this.alertasService.Swal_alert('error', JSON.stringify(res.data));
                  alert(JSON.stringify(res.data));
                }
        })
  }

  cantidadAsignadaOT():any[]{
    const map = new Map();
    const listCheck = this.ordenTrabajoCab.filter((ot)=>ot.checkeado == true).map((detalleOT)=>{
      return {idJefeCuadrilla : detalleOT.idJefeCuadrilla, idEmpresa : detalleOT.idEmpresa }
    })

    let listSocket = [];
    let total =0;
 
   if (this.formParamsFiltro.value.idServicio == 1) { ///---obras se envia la cuadrilla

      const listCuadrilla = [];        
      for (const item of listCheck) {
          if(!map.has(item.idJefeCuadrilla)){
              map.set(item.idJefeCuadrilla, true);    
              listCuadrilla.push({ cuadrilla: item.idJefeCuadrilla });
          }
      } 

      total =0;
      for (const objCuadrilla of listCuadrilla) {          
        total =0;
        for (const datos of listCheck) {
          if (objCuadrilla.cuadrilla == datos.idJefeCuadrilla ) {
            total += 1; 
          }
        }
        listSocket.push({id:objCuadrilla.cuadrilla , cantidadOT :  total, tipo : 'cuadrilla', mensaje : `Usted tiene  ${total} ot, asignada`});
      }
 

   }else{ ///---  se envia la empresa contratista ---

      const listEmpresa = [];        
      for (const item of listCheck) {
          if(!map.has(item.idEmpresa)){
              map.set(item.idEmpresa, true);  
              listEmpresa.push({ empresa: item.idEmpresa });
          }
      }

      for (const objEmpresa of listEmpresa) {          
        total =0;
        for (const datos of listCheck) {
          if (objEmpresa.empresa == datos.idEmpresa ) {
            total += 1; 
          }
        }
        listSocket.push({id : objEmpresa.empresa , cantidadOT :  total , tipo : 'empresa', mensaje : `Usted tiene  ${total} ot, asignada`});        
      }
 
   }

   return listSocket;

  }
  
  enviarOT_jefeCuadrilla(){

    if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
      return 
    }

    if (this.validacionCheckMarcado()==false){
      return;
    } 
    const codigosIdOT = this.funcionGlobalServices.obtenerTodos_IdPrincipal(this.ordenTrabajoCab,'id_OT'); 

    this.alertasService.Swal_Question('Sistemas', 'Esta seguro de enviar las OT al Jefe de Cuadrilla ?')
    .then((result)=>{
      if(result.value){

        Swal.fire({
          icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'
        })
        Swal.showLoading();
        this.ordenTrabajoService.set_enviarOT_jefeCuadrilla(codigosIdOT.join() , this.idUserGlobal ).subscribe((res:RespuestaServer)=>{
          Swal.close();
          if (res.ok) { 
             this.alertasService.Swal_Success("Proceso realizado correctamente..")
             
           ////-- notificaciones Socket para el movil----    
              const listOTSocket  = this.cantidadAsignadaOT();   
  
              this.websocketService.NotificacionOT_WebSocket(listOTSocket)
              .then( (res:any) =>{
                if (res.ok==true) {
                  console.log(res.data);
                }else{
                  this.alertasService.Swal_alert('Error Socket', JSON.stringify(res.data));
                  alert(JSON.stringify(res.data));
                }
              }).catch((error)=>{
                this.alertasService.Swal_alert('Error Socket', JSON.stringify(error));
                alert(JSON.stringify(error));
              })                 
          ////-- Fin de notificaciones Socket para el movil----

               //-----listando la informacion  
             this.mostrarInformacion();  
          }else{
            this.alertasService.Swal_alert('error', JSON.stringify(res.data));
            alert(JSON.stringify(res.data));
          }
        })
 
      }
    })

  }
  

}

