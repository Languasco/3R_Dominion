import { Router, Request, Response	} from 'express';
import Server from '../classes/server';
 
const router = Router();

router.get('/dsigeSocket',( req : Request, res : Response)=>{
    res.json('Probando socket por Api rest');
    const server = Server.Instance;
    server.io.emit('Alertas_web_OT', 'Que fue oie Cachudazo Web jajajaja');
})

//--- body 
// -- x-www-form-urlencoded
// body.cuerpo;
// body.de;
 
 

export default router;