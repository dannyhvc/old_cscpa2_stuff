"use strict";(self["webpackChunk"]=self["webpackChunk"]||[]).push([[847],{9847:(t,s,e)=>{e.r(s),e.d(s,{default:()=>b});var a=e(8613),c=e(5304);const l={class:"text-center q-mt-lg"},o=(0,a._)("div",{class:"text-h4"},"Data Utility",-1),n={class:"status q-mt-md text-subtitle2 text-negative","text-color":"red"};function r(t,s,e,r,i,u){const d=(0,a.up)("q-btn");return(0,a.wg)(),(0,a.iD)("div",l,[o,(0,a.Wm)(d,{class:"q-ma-sm",color:"white","text-color":"black",label:"Load Categories",onClick:r.loadCategories},null,8,["onClick"]),(0,a._)("div",n,(0,c.zw)(r.state.status),1)])}var i=e(8267);const u={setup(){let t=(0,i.qj)({status:""});const s=async()=>{let s="https://localhost:7134/api/Data";try{t.status="resetting casestudy tables ...";let e=await fetch(`${s}`);t.status=await e.json()}catch(e){console.log(e),t.status=`Error has occured: ${e.message}`}};return{loadCategories:s,state:t}}};var d=e(2815),h=e(7357),g=e(8150),m=e.n(g);const p=(0,d.Z)(u,[["render",r]]),b=p;m()(u,"components",{QBtn:h.Z})}}]);