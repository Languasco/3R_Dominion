import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
 

  URL = environment.URL_API;
  estados:any[] = [];
  empresas :any[] = [];

  areas :any[] = [];
  perfiles :any[] = [];

  constructor(private http:HttpClient) { }

  get_estados(){
    if (this.estados.length > 0) {
      return of( this.estados )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '1');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblUsuarios' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.estados = res.data;
                       return res.data;
                  }) );
    }
  }

  get_empresas(){
    if (this.empresas.length > 0) {
      return of( this.empresas )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '2');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblUsuarios' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.empresas = res.data;
                       return res.data;
                  }) );
    }
  }

  get_area(){
    if (this.areas.length > 0) {
      return of( this.areas )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '3');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblUsuarios' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.areas = res.data;
                       return res.data;
                  }) );
    }
  }

  get_perfil(){
    if (this.perfiles.length > 0) {
      return of( this.perfiles )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '7');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblUsuarios' , {params: parametros})
                 .pipe(map((res:any)=>{
                        this.perfiles = res.data;
                       return res.data;
                  }) );
    }
  }


  get_mostrarUsuario_general(idEmpresa:number,idArea:number, idEstado:number){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '4');
    parametros = parametros.append('filtro', idEmpresa + '|' +  idArea  + '|' +  idEstado);

    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros});
  }

  get_verificar_DniPersonal(nroDoc:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', nroDoc);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros}).toPromise();
  }
  
  get_verificar_DniUsuario(nroDoc:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '6');
    parametros = parametros.append('filtro', nroDoc);

    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros}).toPromise();
  }

  get_obtenerDatos_documento(nroDoc:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '8');
    parametros = parametros.append('filtro', nroDoc);

    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros}).toPromise();
  }

  get_verificar_logginUsuario(loggin:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '9');
    parametros = parametros.append('filtro', loggin);

    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros}).toPromise();
  } 

  
  set_saveUsuarios(objUsuarios:any){
    return this.http.post(this.URL + 'tblUsuarios', JSON.stringify(objUsuarios), httpOptions);
  }

  set_editUsuario(objUsuarios:any, idUsuario :number){
    return this.http.put(this.URL + 'tblUsuarios/' + idUsuario , JSON.stringify(objUsuarios), httpOptions);
  }

  set_anularUsuario(idUsuario : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro',  String(idUsuario));

    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros});
  }  

  set_grabarAreasMasivo(idUsuarioBD : number, areas:string, idUser:number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '10');
    parametros = parametros.append('filtro', idUsuarioBD + '|' +  areas  + '|' +  idUser  );
    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros});
  }  

  get_AreasMasivo(idUsuarioBD : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '11');
    parametros = parametros.append('filtro', String(idUsuarioBD) );
    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros});
  }  

  generararDescargar_codigoQr(idUsuario : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '12');
    parametros = parametros.append('filtro',  String(idUsuario));

    console.log(idUsuario)

    return this.http.get( this.URL + 'tblUsuarios' , {params: parametros});
  } 

 
}